using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Common;
using DocRouter.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ICurrentUserService _currentUserService;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="fileStorageService">An implementation of <see cref="IFileStorageService"/></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"/></param>
        /// <param name="logger">An implementation ogf <see cref="ILogger{CreateSubmissionCommandHandler}"/></param>
        /// <param name="mediator">An implemenatation of <see cref="IMediator"/></param>
        /// <param name="currentUserService">An implementation of <see cref="ICurrentUserService"/></param>
        public CreateSubmissionCommandHandler(
            IDocRouterContext context,
            IFileStorageService fileStorageService,
            IDateTime dateTime,
            ILogger<CreateSubmissionCommandHandler> logger,
            IMediator mediator,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _dateTime = dateTime;
            _fileStorageService = fileStorageService;
            _logger = logger;
            _mediator = mediator;
            _currentUserService = currentUserService;
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
                // Create initial submission entity with the fields required to create a directory in storage.
                Submission submissionToAdd = new Submission(
                    request.Title,
                    request.Description,
                    request.DriveId = request.DriveId.Split(',')[0],
                    request.ListId = request.DriveId?.Split(',')?[0] ?? "",
                    new SubmissionTransaction(
                        _dateTime.Now, 
                        _dateTime.Now, 
                        request.Recipient, 
                        _currentUserService.Email, 
                        request.Comments)
                    );
                // Create the directory. The returned entity should have the storage fields populated from the operation that creates the directory.
                Submission folder = await _fileStorageService.CreateDirectoryAsync(submissionToAdd);

                // Next, add each file to the submission
                int fileCount = 0;
                foreach (var file in request.Files)
                {   
                    var fileToAdd = await _fileStorageService.AddFileToDirectoryAsync(folder, file);
                    fileCount++;
                    fileToAdd.UpdateDisplayOrder(fileCount);
                    folder.AddItem(fileToAdd);
                }
                await _context.Submissions.AddAsync(folder);
                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new SubmissionCreated 
                    { 
                        SubmissionId = folder.Id, 
                        SubmissionUri = folder.FolderUri,
                        SubmissionTitle = folder.Title, 
                        SubmittedBy = folder.Transactions.First().RoutedFrom,
                        SubmittedTo = folder.Transactions.First().RoutedTo
                });
                return new Result(true, folder.FolderUri, new List<string>());
            }
            catch (Exception ex)
            {
                _logger.LogError("CreateSubmissionHandler error: {0}", ex.Message);
                return Result.Failure(new List<string>() { ex.Message });
            }

        }
    }
}
