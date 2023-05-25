using Azure.Identity;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Drives.Item.Items.Item.CreateUploadSession;
using Microsoft.Graph.Drives.Item.Items.Item.Invite;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocRouter.Infrastructure.Azure
{
    // TODO: Move Drive IDs to AzureADSettings?
    public class AzureADFileStorageService : IFileStorageService
    {
        private GraphServiceClient graphClient;
        private readonly ILogger _logger;
        public AzureADFileStorageService(AzureAD settings, ILogger<AzureADFileStorageService> logger)
        {
            var clientSecretCredential = new ClientSecretCredential(settings.TenantId, settings.ClientId, settings.ClientSecret);
            graphClient = new GraphServiceClient(clientSecretCredential);
            _logger = logger;
        }

        public async Task<FileResult> AddFileToDirectoryAsync(string directoryName, FileSubmissionDto file)
        {
            DriveItem uploadedFile = null;
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
            var uploadSession = await graphClient.Drives["b!j3sReZftLkuOgOZFHws5jx8M56sz-i9IkpjRrPkNncnrKJubNalgR6RgYT57FY72"]
                .Items[directoryName]
                .ItemWithPath(file.FileName)
                .CreateUploadSession.PostAsync(uploadSessionRequestBody);
            // Max slice size must be a multiple of 320 KiB
            int maxSliceSize = 320 * 1024;
            var fileUploadTask = new LargeFileUploadTask<DriveItem>(uploadSession, file.Content, maxSliceSize, graphClient.RequestAdapter);
            var totalLength = file.Content.Length;
            // Create a callback that is invoked after each slice is uploaded
            IProgress<long> progress = new Progress<long>(prog => {
                _logger.LogInformation($"AzureADFileStorageService: Uploaded {prog} bytes of {totalLength} bytes");
            });
            try
            {
                // Upload the file
                var uploadResult = await fileUploadTask.UploadAsync(progress);
                var result = new FileResult
                {
                    Uri = uploadResult.ItemResponse.WebUrl,
                    Id  = uploadResult.ItemResponse.Id
                };
                _logger.LogInformation(uploadResult.UploadSucceeded ?
                    $"Upload complete, item ID: {uploadResult.ItemResponse.Id}" :
                    "Upload failed");
                return result;
            }
            catch (ServiceException ex)
            {
                _logger.LogError($"AzureADFileStorageService Error uploading: {ex.ToString()}");
                throw ex;
            }
        }

        public async Task<DirectoryResult> CreateDirectoryAsync(string submittedByEmail, string submittedToEmail)
        {
            Guid folderUUID = Guid.NewGuid();
            var requestBody = new DriveItem
            {
                Name = folderUUID.ToString(),
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
                    new DriveRecipient{ Email = submittedByEmail },
                    new DriveRecipient{ Email = submittedToEmail }
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
                _logger.LogInformation($"Attempting to create remote directory with name {folderUUID}");
                var r = await graphClient.Drives["b!j3sReZftLkuOgOZFHws5jx8M56sz-i9IkpjRrPkNncnrKJubNalgR6RgYT57FY72"].Items["013TPQOTV6Y2GOVW7725BZO354PWSELRRZ"].Children.PostAsync(requestBody);
                _logger.LogInformation($"Attempting to set permissions for users: {submittedByEmail}, {submittedToEmail} on folder {r.Id}");
                var permissionResult = await graphClient.Drives["b!j3sReZftLkuOgOZFHws5jx8M56sz-i9IkpjRrPkNncnrKJubNalgR6RgYT57FY72"].Items[r.Id].Invite.PostAsync(permissionsRequestBody);
                return new DirectoryResult
                {
                    Name = r.Name,
                    Uri = r.WebUrl,
                    Id = r.Id
                };
            }
            catch(Exception e)
            {
                _logger.LogError($"AzureADFileStorageService exception: {e.Message}");
                throw e;
            }
        }
        public async Task DeleteDirectoryAsync(string directoryId)
        {
            _logger.LogInformation($"FileStorageService.DeleteDirectoryAsync invoked with Id: {directoryId}");
            try
            {
                await graphClient.Drives["b!j3sReZftLkuOgOZFHws5jx8M56sz-i9IkpjRrPkNncnrKJubNalgR6RgYT57FY72"].Items[directoryId].DeleteAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"AzureADFileStorageService exception: {e.Message}");
                throw e;
            }
        }

        public Task DeleteFileAsync(string fileId)
        {
            throw new NotImplementedException();
        }
    }
}
