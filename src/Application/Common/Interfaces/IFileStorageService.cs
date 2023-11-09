using DocRouter.Application.Common.Models;
using DocRouter.Domain.Entities;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Interfaces
{
    /// <summary>
    /// Interface that defines a file storage service.
    /// </summary>
    public interface IFileStorageService //TODO: all methods on this interface should use Entity 
    {
        /// <summary>
        /// Creates a new Directory in the provided Drive.
        /// </summary>
        /// <param name="submission">The submission that is to be created as a Directory.</param>
        /// <returns>A <see cref="DirectoryResult"/> object containing details of the new directory.</returns>
        Task<Submission> CreateDirectoryAsync(Submission submission);
        /// <summary>
        /// Adds a new file to an existing directory.
        /// </summary>
        /// <param name="submission">A <see cref="Submission"/> object representing the target folder.</param>
        /// <param name="file">A <see cref="FileSubmissionDto"/> object representing the file to be added to the target folder.</param>
        /// <returns>The <see cref="SubmissionItem"/> object returned with details of the uploaded file populated.</returns>
        Task<SubmissionItem> AddFileToDirectoryAsync(Submission submission, FileSubmissionDto file);
        /// <summary>
        /// Deletes a directory
        /// </summary>
        /// <param name="submission">The submission of the item to be deleted.</param>
        /// <returns></returns>
        Task DeleteDirectoryAsync(Submission submission);
        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="file">The Submission Item to be deleted.</param>
        /// <returns></returns>
        Task DeleteFileAsync(SubmissionItem file);
        /// <summary>
        /// Downloads a file as a pdf.
        /// </summary>
        /// <param name="file">The <see cref="SubmissionItem"/> to download.</param>
        /// <returns>A <see cref="FileSubmissionDto"/> object.</returns>
        Task<FileSubmissionDto> DownloadFileAsPdfAsync(SubmissionItem file);
        /// <summary>
        /// Downloads all files in a folder and combines them into a single document.
        /// </summary>
        /// <param name="submission">The submission object containing the documents to combine.</param>
        /// <returns>A <see cref="FileSubmissionDto"/> containing all the files combined into a single .pdf</returns>
        Task<FileSubmissionDto> DownloadCombinedPdfFile(Submission submission);
    }
    /// <summary>
    /// Class that contains details of a File Result.
    /// </summary>
    public class FileResult
    {
        /// <summary>
        /// The URI of the file.
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// The Id of the file.
        /// </summary>
        public string Id { get; set; }
    }
    /// <summary>
    /// Class that contains details of a Directory Result.
    /// </summary>
    public class DirectoryResult
    {
        /// <summary>
        /// The name of the directory.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The URI of the directory.
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// The Id of the directory.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The List Id of the directory.
        /// </summary>
        public string ListId { get; set; }
    }
}
