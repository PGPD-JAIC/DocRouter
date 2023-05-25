using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Common;
using DocRouter.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.CreateSubmission
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> that handles a request to create a file submission.
    /// </summary>
    public class CreateSubmissionCommandHandler : IRequestHandler<CreateSubmissionCommand, Result>
    {
        private readonly IDocRouterContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IDateTime _dateTime;
        private readonly ILogger<CreateSubmissionCommandHandler> _logger;
        private readonly IMediator _mediator;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="fileStorageService">An implementation of <see cref="IFileStorageService"/></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"/></param>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public CreateSubmissionCommandHandler(
            IDocRouterContext context,
            IFileStorageService fileStorageService,
            IDateTime dateTime,
            ILogger<CreateSubmissionCommandHandler> logger,
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
        /// <param name="request">A <see cref="CreateSubmissionCommand"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>a <see cref="Result"/></returns>
        public async Task<Result> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Remove hard-coded value
                DirectoryResult folder = await _fileStorageService.CreateDirectoryAsync(request.Recipient, "jcsmith1@co.pg.md.us");
                var submission = new Submission(request.Title, request.Description, folder.Uri, folder.Id);

                foreach (var file in request.Files)
                {
                    FileResult fileResult = await _fileStorageService.AddFileToDirectoryAsync(folder.Id, file);
                    var submissionItem = new SubmissionItem(file.FileName, fileResult.Uri, fileResult.Id);
                    submission.AddItem(submissionItem);
                }
                var transaction = new SubmissionTransaction(_dateTime.Now, _dateTime.Now, DocRouter.Common.Enums.TransactionStatus.Pending, request.Recipient, request.Comments);
                submission.AddTransaction(transaction);
                await _context.Submissions.AddAsync(submission);
                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new SubmissionCreated 
                    { 
                        SubmissionId = submission.Id, 
                        SubmissionUri = submission.FolderUri,
                        SubmissionTitle = submission.Title, 
                        SubmittedBy = submission.CreatedBy,
                        SubmittedTo = request.Recipient
                    });
                return new Result(true, submission.FolderUri, new List<string>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateSubmissionHandler error: {ex.Message}");
                return Result.Failure(new List<string>() { ex.Message });
            }

        }
    }
}
