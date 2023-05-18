using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DocRouter.Infrastructure
{
    /// <summary>
    /// Implementation of <see cref="IFileStorageService"/> that uses local storage to store files.
    /// </summary>
    public class FileStorageService : IFileStorageService
    {
        // TODO: Move the path into configuration.
        private static readonly string _rootPath = @"C:\DocRouter";
        private readonly ILogger _logger;
        public FileStorageService(ILogger<FileStorageService> logger)
        {
            _logger = logger;
            _logger.LogInformation($"File Storage Service initiated; Root Path is {_rootPath}");
        }
        /// <inheritdoc />
        public async Task<DirectoryResult> CreateDirectoryAsync(string submittedByEmail, string submittedToEmail)
        {
            Guid folderUUID = Guid.NewGuid();
            string fullPath = Path.Combine(_rootPath, folderUUID.ToString());
            try
            {
                if (Directory.Exists(fullPath))
                {
                    _logger.LogInformation($"Attempting to create Directory at {fullPath}");
                    throw new ArgumentException($"Cannot create directory: {folderUUID} already exists.");
                }
                DirectoryInfo newDirectory = Directory.CreateDirectory(fullPath);
                _logger.LogInformation($"Directory was successfully created at {fullPath}");
                
                return new DirectoryResult
                {
                    Name = folderUUID.ToString(),
                    Uri = fullPath,
                    Id = folderUUID.ToString()
                };

            }
            catch (Exception e)
            {
                _logger.LogError($"FileStorageService.CreateDirectoryAsync error: {e.Message}");
                throw e;
            }
        }
        /// <inheritdoc />
        public async Task<FileResult> AddFileToDirectoryAsync(string directoryName, FileSubmissionDto file)
        {
            
            _logger.LogInformation($"FileStorageService.AddFilesToDirectoryAsync invoked with path {directoryName}");
            if (!Directory.Exists(directoryName))
            {
                throw new DirectoryNotFoundException(directoryName);
            }
            else
            {
                directoryName = Path.Combine(_rootPath, directoryName);
            }
            try
            {
                using (var fileStream = File.Create(Path.Combine(directoryName, file.FileName)))
                {
                    file.Content.Seek(0, SeekOrigin.Begin);
                    await file.Content.CopyToAsync(fileStream);
                }
                return new FileResult
                {
                    Uri = Path.Combine(directoryName, file.FileName)
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"FileStorageService.CreateDirectoryAsync error: {e.Message}");
                throw e;
            }
        }
    }
}
