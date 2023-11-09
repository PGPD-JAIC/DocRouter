using DocRouter.Application.Common.Exceptions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.DeleteSubmission
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> that handles a request to delete a file submission.
    /// </summary>
    public class DeleteSubmissionCommandHandler : IRequestHandler<DeleteSubmissionCommand, Result>
    {
        private readonly IDocRouterContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IDateTime _dateTime;
        private readonly ILogger<DeleteSubmissionCommandHandler> _logger;
        private readonly IMediator _mediator;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="fileStorageService">An implementation of <see cref="IFileStorageService"/></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"/></param>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public DeleteSubmissionCommandHandler(
            IDocRouterContext context,
            IFileStorageService fileStorageService,
            IDateTime dateTime,
            ILogger<DeleteSubmissionCommandHandler> logger,
            IMediator mediator)
        {
            _context = context;
            _dateTime = dateTime;
            _fileStorageService = fileStorageService;
            _logger = logger;
            _mediator = mediator;

        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="DeleteSubmissionCommand"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>a <see cref="Result"/></returns>
        public async Task<Result> Handle(DeleteSubmissionCommand request, CancellationToken cancellationToken)
        {   
            var toDelete = await _context.Submissions.FindAsync(request.Id);
            if (toDelete == null)
            {
                throw new NotFoundException($"No submission with Id: {request.Id} could be found.", nameof(request.Id));
            }
            try
            {
                await _fileStorageService.DeleteDirectoryAsync(toDelete);
                _context.Submissions.Remove(toDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteSubmissionCommandHandler encountered an error when trying to delete directory with id: {request.Id}: {ex.Message}");
                throw new BadRequestException("Could not complete delete operation.");
            }
            return Result.Success();
        }
    }
}
