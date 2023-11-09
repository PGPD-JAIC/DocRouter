using DocRouter.Application.Common.Exceptions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetCombinedFile
{
    /// <summary>
    /// Implentation of <see cref="IRequestHandler{GetCombinedFileQuery, FileSubmissionDto}"/>
    /// </summary>
    public class GetCombinedFileQueryHandler : IRequestHandler<GetCombinedFileQuery, FileSubmissionDto>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IDocRouterContext _context;
        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="fileStorageService">An implementation of <see cref="IFileStorageService"/></param>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        public GetCombinedFileQueryHandler(IFileStorageService fileStorageService, IDocRouterContext context)
        {
            _fileStorageService = fileStorageService;
            _context = context;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetCombinedFileQuery"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="FileSubmissionDto"/> object.</returns>
        public async Task<FileSubmissionDto> Handle(GetCombinedFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _context.Submissions
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new NotFoundException("File", request.Id); ;
            return await _fileStorageService.DownloadCombinedPdfFile(file);
        }
    }
}
