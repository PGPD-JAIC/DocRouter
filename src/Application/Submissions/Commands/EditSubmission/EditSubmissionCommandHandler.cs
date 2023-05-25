using DocRouter.Application.Common.Exceptions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Application.Submissions.Commands.CreateSubmission;
using DocRouter.Application.Submissions.Queries.GetEditSubmissionDetail;
using DocRouter.Common;
using DocRouter.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.EditSubmission
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> that handles a request to edit a file submission.
    /// </summary>
    public class EditSubmissionCommandHandler : IRequestHandler<EditSubmissionCommand, Result>
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
        public EditSubmissionCommandHandler(
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
        /// <param name="request">A <see cref="EditSubmissionCommand"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>a <see cref="Result"/></returns>
        public async Task<Result> Handle(EditSubmissionCommand command, CancellationToken cancellationToken)
        {
            var toEdit = await _context.Submissions
                .Include(x => x.Items)
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync(x => x.Id == command.Id);

            if (toEdit == null)
            {
                throw new NotFoundException($"No submission with Id : {command.Id} could be found.", command.Id);
            }
            try
            {
                // Update Title, if needed
                if (toEdit.Title != command.Title)
                {
                    _logger.LogInformation($"Updating Submission [{toEdit.Id}] Title from [{toEdit.Title}] to [{command.Title}].");
                    toEdit.UpdateTitle(command.Title);
                }
                // Update Description, if needed
                if (toEdit.Description != command.Description)
                {
                    _logger.LogInformation($"Updating Submission [{toEdit.Id}] Description from [{toEdit.Description}] to [{command.Description}].");
                    toEdit.UpdateDescription(command.Description);
                }
                // Update routing, if required.
                if (command.CurrentlyRoutedTo != command.NewRoutedTo)
                {
                    _logger.LogInformation($"Updating Submission [{toEdit.Id}] Routing from [{command.CurrentlyRoutedTo}] to [{command.NewRoutedTo}].");
                    // Find the newest transaction
                    _logger.LogInformation($"Retrieving Submission [{toEdit.Id}] newest transaction.");
                    var xActionToRecall = toEdit.Transactions.OrderByDescending(t => t.TransactionDate).First();
                    // Recall the newest transaction
                    _logger.LogInformation($"Recalling Transaction [{xActionToRecall.Id}].");
                    xActionToRecall.UpdateTransactionStatus(DocRouter.Common.Enums.TransactionStatus.Recalled);
                    // Add a newer transaction with the updated recipient.
                    _logger.LogInformation($"Adding new Transaction.");
                    toEdit.AddTransaction(new SubmissionTransaction(_dateTime.Now, _dateTime.Now, DocRouter.Common.Enums.TransactionStatus.Pending, command.NewRoutedTo, xActionToRecall.Comments));
                }
                // Add any new Files
                if (command.FilesToAdd.Count > 0)
                {
                    _logger.LogInformation($"Adding [{command.FilesToAdd.Count}] to Submission [{toEdit.Id}].");
                    foreach (var file in command.FilesToAdd)
                    {
                        _logger.LogInformation($"Attempting to add [{file.FileName}] to Directory Id: [{toEdit.UniqueId}]");
                        FileResult fileResult = await _fileStorageService.AddFileToDirectoryAsync(toEdit.UniqueId, file);
                        _logger.LogInformation($"File [{file.FileName}] successfully added to Directory Id: [{toEdit.UniqueId}] as Id: [{fileResult.Id}]");
                        _logger.LogInformation($"Adding file to Submission Id: [{toEdit.Id}]");
                        var submissionItem = new SubmissionItem(file.FileName, fileResult.Uri, fileResult.Id);
                        toEdit.AddItem(submissionItem);
                    }
                }
                // Remove any files
                if (command.FilesToRemove.Count > 0)
                {
                    _logger.LogInformation($"Removing [{command.FilesToRemove.Count}] from Submission [{toEdit.Id}].");
                    foreach (int id in command.FilesToRemove)
                    {
                        _logger.LogInformation($"Attempting to locate file id: [{id}] in Submission [{toEdit.Id}]'s file collection.");
                        var item = toEdit.Items.First(i => i.Id == id);
                        if (item != null)
                        {
                            _logger.LogInformation($"File Id: [{id}] found, attemping to remove from storage via id: [{item.UniqueId}]");
                            toEdit.RemoveItem(item);
                            await _fileStorageService.DeleteFileAsync(item.UniqueId);
                        }
                    }
                }
                return new Result(true, toEdit.FolderUri, new List<string>());
            }
            catch(Exception ex)
            {
                _logger.LogError($"EditSubmissionCommand error: {ex.Message}");
                return Result.Failure(new List<string>() { ex.Message });
            }
        }
    }
}
