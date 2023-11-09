using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DocRouter.Application.UnitTests.Common
{
    public class TestFileService : IFileStorageService
    {
        private static readonly string _rootPath = @"C:\DocRouter";

        public Task<SubmissionItem> AddFileToDirectoryAsync(Submission submission, FileSubmissionDto file)
        {
            throw new NotImplementedException();
        }

        public Task<Submission> CreateDirectoryAsync(Submission submission)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDirectoryAsync(Submission submission)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFileAsync(SubmissionItem file)
        {
            throw new NotImplementedException();
        }

        public Task<FileSubmissionDto> DownloadCombinedPdfFile(Submission submission)
        {
            throw new NotImplementedException();
        }

        public Task<FileSubmissionDto> DownloadFileAsPdfAsync(SubmissionItem file)
        {
            throw new NotImplementedException();
        }
    }
}
