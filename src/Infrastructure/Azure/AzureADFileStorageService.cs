using Azure.Identity;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Drives.Item.Items.Item.CreateUploadSession;
using Microsoft.Graph.Drives.Item.Items.Item.Invite;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DocRouter.Infrastructure.Azure
{
    /// <summary>
    /// Implementation of <see cref="IFileStorageService"/> that interacts with files stored in Azure/Sharepoint.
    /// </summary>
    public class AzureADFileStorageService : IFileStorageService
    {
        private readonly GraphServiceClient graphClient;
        private readonly ILogger _logger;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="settings">A <see cref="AzureADSettings"/> object that contains configuration details for the targeted Azure deployment.</param>
        /// <param name="logger">An implementation of <see cref="ILogger{AzureADFileStorageService}"></see></param>
        public AzureADFileStorageService(AzureADSettings settings, ILogger<AzureADFileStorageService> logger)
        {
            var clientSecretCredential = new ClientSecretCredential(settings.TenantId, settings.ClientId, settings.ClientSecret);
            graphClient = new GraphServiceClient(clientSecretCredential);
            _logger = logger;
        }
        /// <summary>
        /// Creates a new Directory in the provided Drive.
        /// </summary>
        /// <param name="submission">The <see cref="Submission"/> item to be added to the Document Library.</param>
        /// <remarks>
        /// The following fields of the <paramref name="submission"/> are required to create a directory:
        /// <list type="bullet">
        /// <item>
        /// <term>Submission.Title</term>
        /// <description>Used as the title of the folder created in the document library.</description>
        /// </item>
        /// <item>
        /// <term>Submission.Transactions[0].RoutedTo</term>
        /// <description>Used to set access permissions on the DriveItem.</description>
        /// </item>
        /// <item>
        /// <term>Submission.Transactions[0].RoutedFrom</term>
        /// <description>Used to set access permissions on the DriveItem.</description>
        /// </item>
        /// <item>
        /// <term>Submission.DriveId</term>
        /// <description>Required for the API call.</description>
        /// </item>
        /// <item>
        /// <term>Submission.ListId</term>
        /// <description>Required for the API call.</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="ODataError">
        /// Thrown when an error is returned from MSGraph.
        /// </exception>
        /// <returns>A <see cref="Submission"/> object with the relevant fields from the MSGraph action populated.</returns>
        public async Task<Submission> CreateDirectoryAsync(Submission submission)
        {   
            var requestBody = new DriveItem
            {
                Name = submission.Title,
                Folder = new Folder
                {
                },
                AdditionalData = new Dictionary<string, object>
                {
                    {
                        "@microsoft.graph.conflictBehavior" , "rename"
                    },
                }
            };
            var permissionsRequestBody = new InvitePostRequestBody
            {
                Recipients = new List<DriveRecipient>
                {
                    new DriveRecipient{ Email = submission.Transactions.First().RoutedTo },
                    new DriveRecipient{ Email = submission.Transactions.First().RoutedFrom }
                },
                RequireSignIn = true,
                SendInvitation = false,
                Roles = new List<string>
                {
                    "read",
                    "write"
                }
            };
            try
            {
                _logger.LogInformation("Attempting to create remote directory with name {0}", submission.Title);
                var r = await graphClient.Drives[submission.DriveId].Items[submission.ListId].Children.PostAsync(requestBody);
                _logger.LogInformation("Attempting to set permissions for users: {0}, {1} on folder {2}", submission.Transactions.First().RoutedTo, submission.Transactions.First().RoutedFrom, r.Id);
                var permissionResult = await graphClient.Drives[submission.DriveId].Items[r.Id].Invite.PostAsync(permissionsRequestBody);
                submission.UpdateFolderUri(r.WebUrl);
                submission.UpdateItemId(r.Id);

                return submission;
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.NotFound) // hopefully, catch if the Directory is not found
            {
                _logger.LogError("No Drive with Id {0} was found.", submission.DriveId); // TODO: Replace with specific Drive Id.
                throw (odataError);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Unauthorized)
            {
                _logger.LogError("Current user is not authorized to create directories in drive: {0}", submission.DriveId); // TODO: Replace with specific Drive Id.
                throw odataError;
            }
            catch (ODataError odataError)
            {
                _logger.LogError($"AzureADFileStorageService error: StatusCode: {0} | Message: {1}", odataError.ResponseStatusCode, odataError.Message);
                throw odataError;
            }
        }
        /// <summary>
        /// Adds a new file to an existing directory.
        /// </summary>
        /// <param name="submission">A <see cref="Submission"/> object representing the target folder.</param>
        /// <param name="file">A <see cref="FileSubmissionDto"/> object containing the details of the file to upload.</param>
        /// <exception cref="ODataError">
        /// Thrown when: no folder with a name matching the uniqueId property of the provided <paramref name="submission"/> was found in the drive or the user is not authorized to create files in the drive/directory
        /// </exception>
        /// <returns>A <see cref="SubmissionItem"/> object containing details of the uploaded file.</returns>
        public async Task<SubmissionItem> AddFileToDirectoryAsync(Submission submission, FileSubmissionDto file)
        {   
            // Use properties to specify the conflict behavior
            // in this case, replace
            var uploadSessionRequestBody = new CreateUploadSessionPostRequestBody
            {
                Item = new DriveItemUploadableProperties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "@microsoft.graph.conflictBehavior", "replace" }
                    }
                }
            };
            try
            {
                var uploadSession = await graphClient.Drives[submission.DriveId]
                    .Items[submission.ItemId]
                    .ItemWithPath(file.FileName)
                    .CreateUploadSession.PostAsync(uploadSessionRequestBody);
                // Max slice size must be a multiple of 320 KiB
                int maxSliceSize = 320 * 1024;
                var fileUploadTask = new LargeFileUploadTask<DriveItem>(uploadSession, file.Content, maxSliceSize, graphClient.RequestAdapter);
                var totalLength = file.Content.Length;
                // Create a callback that is invoked after each slice is uploaded
                IProgress<long> progress = new Progress<long>(prog => {
                    _logger.LogInformation("AzureADFileStorageService: Uploaded {0} bytes of {1} bytes", prog, totalLength);
                });
                // Upload the file
                var uploadResult = await fileUploadTask.UploadAsync(progress);
                var result = new FileResult
                {
                    Uri = uploadResult.ItemResponse.WebUrl,
                    Id = uploadResult.ItemResponse.Id
                };
                _logger.LogInformation(uploadResult.UploadSucceeded ?
                    "Upload complete, item ID: {0}" :
                    "Upload failed", uploadResult.ItemResponse.Id);
                var returnItem = new SubmissionItem(file.FileName, uploadResult.ItemResponse.WebUrl, uploadResult.ItemResponse.Id, submission.DriveId);
                return returnItem;
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.NotFound) // hopefully, catch if the Directory is not found
            {
                _logger.LogError("Directory with name {0} not found in drive: {1}", submission.ItemId, submission.DriveId); // TODO: Replace with specific Drive Id.
                throw(odataError);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Unauthorized)
            {
                _logger.LogError("Current user is not authorized to create files in directory: {0}", submission.ItemId); // TODO: Replace with specific Drive Id.
                throw odataError;
            }
            catch (ODataError odataError)
            {
                _logger.LogError("AzureADFileStorageService error: StatusCode: {0} | Message: {1}", odataError.ResponseStatusCode, odataError.Message);
                throw odataError;
            }
        }
        /// <summary>
        /// Deletes a directory
        /// </summary>
        /// <param name="submission">The <see cref="Submission"/> representing the directory to be deleted.</param>
        /// <returns></returns>
        public async Task DeleteDirectoryAsync(Submission submission)
        {
            _logger.LogInformation("FileStorageService.DeleteDirectoryAsync invoked with Id: {0}", submission.ItemId);
            try
            {
                await graphClient.Drives[submission.DriveId].Items[submission.ItemId].DeleteAsync();
            }
            catch (ODataError odataError)
            {
                _logger.LogError("Unable to delete directory: {0} from Drive: {1}", submission.ItemId, submission.DriveId);
                throw odataError;
            }
        }
        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="toDelete">The <see cref="SubmissionItem"/> to be deleted.</param>
        /// <returns></returns>
        public async Task DeleteFileAsync(SubmissionItem toDelete)
        {
            try
            {
                await graphClient.Drives[toDelete.DriveId].Items[toDelete.ItemId].DeleteAsync();
            }
            catch (ODataError odataError)
            {
                _logger.LogError("Unable to delete file: {0} from Drive: {1}", toDelete.ItemId, toDelete.DriveId);
                throw odataError;
            }
        }
        /// <summary>
        /// Downloads a file as a pdf.
        /// </summary>
        /// <param name="file">The <see cref="SubmissionItem"/> representing the file to be downloaded.</param>
        /// <returns>A <see cref="FileSubmissionDto"/> object.</returns>
        public async Task<FileSubmissionDto> DownloadFileAsPdfAsync(SubmissionItem file) {
            // TODO: How to handle filetypes that cannot be converted to .pdf?
            // TODO: See this link for potential answer to download all items in folder: https://stackoverflow.com/questions/57034538/download-and-upload-driveitem-from-shared-onedrive-folder-with-ms-graph-sdk/57165245#57165245
            
            // workaround a bug in the dotnet SDK here: https://github.com/microsoftgraph/msgraph-sdk-dotnet/issues/1621
            var requestInformation = graphClient.Drives[file.DriveId].Items[file.ItemId].Content.ToGetRequestInformation();
            requestInformation.UrlTemplate += "{?format}"; // Add the format query parameter to the template and query parameter.
            requestInformation.QueryParameters.Add("format", "pdf");
            
            var fileItem = await graphClient.RequestAdapter.SendPrimitiveAsync<Stream>(requestInformation);
            
            var result = new FileSubmissionDto
            {
                FileName = Path.ChangeExtension(file.ItemName, ".pdf"),
                Content = fileItem,
                ContentType = "application/pdf"
            };
            return result;
        }
        /// <summary>
        /// Downloads all documents in a folder into a single .pdf.
        /// </summary>
        /// <param name="submission">The <see cref="Submission"/> containing the files.</param>
        /// <returns>A <see cref="FileSubmissionDto"/> containing the combined file.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<FileSubmissionDto> DownloadCombinedPdfFile(Submission submission)
        {
            // TODO: How to allow user to specify the order that each document appears in the combined file?
            // TODO: Dispose of temp files
            var fileSubmissions = new List<string>();
            // Loop through the .Value collection and invoke the .DownloadFileAsync on each
            try
            {
                foreach (SubmissionItem item in submission.Items.OrderBy(x => x.DisplayOrder))
                {
                    // workaround a bug in the dotnet SDK here: https://github.com/microsoftgraph/msgraph-sdk-dotnet/issues/1621
                    var requestInformation = graphClient.Drives[item.DriveId].Items[item.ItemId].Content.ToGetRequestInformation();
                    requestInformation.UrlTemplate += "{?format}"; // Add the format query parameter to the template and query parameter.
                    requestInformation.QueryParameters.Add("format", "pdf");

                    var fileItem = await graphClient.RequestAdapter.SendPrimitiveAsync<Stream>(requestInformation);
                    var newFileName = System.IO.Path.ChangeExtension(item.ItemName, ".pdf");
                    string tempFilePath = Path.Combine(Path.GetTempPath(), newFileName);
                    using (var fileStream = new FileStream(tempFilePath, FileMode.Create, System.IO.FileAccess.Write))
                        fileItem.CopyTo(fileStream);
                    fileSubmissions.Add(tempFilePath);
                }
            }
            catch (ODataError odataError)
            {
                _logger.LogError("Could not download/convert file. Error: {0} | Message: {1}", odataError.ResponseStatusCode, odataError.Message);
                throw odataError;
            }

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();
            try
            {
                // Iterate files
                foreach (string file in fileSubmissions)
                {
                    // Open the document to import pages from it.
                    PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                    // Iterate pages
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                    }
                }

                // Save the document...
                outputDocument.Save(submission.Title);
                var fileStream = new FileStream(
                    submission.Title, FileMode.Open, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose);
                
                if (System.IO.File.Exists(submission.Title))
                {
                    return new FileSubmissionDto
                    {
                        FileName = submission.Title + ".pdf",
                        Content = fileStream,
                        ContentType = "application/pdf"
                    };
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not combine files.");
                throw ex;
            }
        }
    }
}
