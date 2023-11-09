using DocRouter.Application.Common.Exceptions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetFile
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{GetFileQuery, FileSubmissionDto}"/>
    /// </summary>
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileSubmissionDto>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IDocRouterContext _context;

        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="fileStorageService">An implementation of <see cref="IFileStorageService"/></param>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        public GetFileQueryHandler(IFileStorageService fileStorageService, IDocRouterContext context)
        {
            _fileStorageService = fileStorageService;
            _context = context;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetFileQuery"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="FileSubmissionDto"/> object.</returns>
        public async Task<FileSubmissionDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _context.Items.FindAsync(request.Id) ?? throw new NotFoundException("File", request.Id);
            return await _fileStorageService.DownloadFileAsPdfAsync(file);
        }
    }
}
