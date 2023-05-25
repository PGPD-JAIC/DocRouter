using DocRouter.Application.Common.Models;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Interfaces
{
    /// <summary>
    /// Interface that defines a file storage service.
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Create a directory and add files.
        /// </summary>
        /// <returns>A string containing the path to the new directory.</returns>
        Task<DirectoryResult> CreateDirectoryAsync(string submittedByEmail, string submittedToEmail);
        /// <summary>
        /// Adds a file to a directory at the given path and returns the full path to the file.
        /// </summary>
        /// <param name="directoryName">A string containing the path to the directory in which the file is to be added.</param>
        /// <param name="file">A <see cref="FileSubmissionDto"/> containing file details.</param>
        /// <returns>A string containing the full path to the added file.</returns>
        Task<FileResult> AddFileToDirectoryAsync(string directoryName, FileSubmissionDto file);
        /// <summary>
        /// Deletes a directory.
        /// </summary>
        /// <param name="directoryId"></param>
        /// <returns></returns>
        Task DeleteDirectoryAsync(string directoryId);
        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task DeleteFileAsync(string fileId);
    }
    public class FileResult
    {
        public string Uri { get; set; }
        public string Id { get; set; }
    }
    public class DirectoryResult
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Id { get; set; }
    }
}
