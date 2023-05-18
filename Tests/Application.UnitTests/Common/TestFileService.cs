using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DocRouter.Application.UnitTests.Common
{
    public class TestFileService : IFileStorageService
    {
        private static readonly string _rootPath = @"C:\DocRouter";
        public Task<FileResult> AddFileToDirectoryAsync(string directoryName, FileSubmissionDto file)
        {
            return Task.Run(() => (new FileResult { Uri = Path.Combine(directoryName, file.FileName) }));
        }

        public Task<DirectoryResult> CreateDirectoryAsync(string submittedByEmail, string submittedToEmail)
        {
            Guid newGUID = Guid.NewGuid();
            return Task.Run(() => (new DirectoryResult { Uri = Path.Combine(_rootPath, newGUID.ToString()) }));
        }
    }
}
