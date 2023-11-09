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
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="fileStorageService">An implementation of <see cref="IFileStorageService"/></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger{CreateSubmissionCommandHandler}"/>.</param>
        /// <param name="currentUserService">An implementation of <see cref="ICurrentUserService"/>.</param>
        /// <param name="mediator">An Implementation of <see cref="IMediator"/></param>
        public EditSubmissionCommandHandler(
            IDocRouterContext context,
            IFileStorageService fileStorageService,
            IDateTime dateTime,
            ILogger<CreateSubmissionCommandHandler> logger,
            ICurrentUserService currentUserService,
            IMediator mediator)
        {
            _context = context;
            _dateTime = dateTime;
            _fileStorageService = fileStorageService;
            _logger = logger;
            _currentUserService = currentUserService;   
            _mediator = mediator;

        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="command">A <see cref="EditSubmissionCommand"/> object.</param>
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
                throw new NotFoundException("No submission with Id : {0} could be found.", command.Id);
            }
            try
            {
                // Update Title, if needed
                if (toEdit.Title != command.Title)
                {
                    _logger.LogInformation("Updating Submission [{0}] Title from [{1}] to [{2}].", toEdit.Id, toEdit.Title, command.Title);
                    toEdit.UpdateTitle(command.Title);
                }
                // Update Description, if needed
                if (toEdit.Description != command.Description)
                {
                    _logger.LogInformation("Updating Submission [{0}] Description from [{1}] to [{2}].", toEdit.Id, toEdit.Description, command.Description);
                    toEdit.UpdateDescription(command.Description);
                }
                // Update routing, if required.
                if (command.CurrentlyRoutedTo != command.NewRoutedTo)
                {
                    _logger.LogInformation("Updating Submission [{0}] Routing from [{1}] to [{2}].", toEdit.Id, command.CurrentlyRoutedTo, command.NewRoutedTo);
                    // Find the newest transaction
                    _logger.LogInformation("Retrieving Submission [{0}] newest transaction.", toEdit.Id);
                    var xActionToRecall = toEdit.Transactions.OrderByDescending(t => t.Created).First();
                    // Recall the newest transaction
                    _logger.LogInformation("Recalling Transaction [{0}].", xActionToRecall.Id);
                    xActionToRecall.Recall(_dateTime.Now);
                    // Add a newer transaction with the updated recipient.
                    _logger.LogInformation($"Adding new Transaction.");
                    toEdit.AddTransaction(new SubmissionTransaction(
                        _dateTime.Now, 
                        _dateTime.Now, 
                        command.NewRoutedTo, 
                        _currentUserService.Email, 
                        command.Comments)
                        );
                }
                // Add any new Files
                if (command.FilesToAdd.Count > 0)
                {
                    _logger.LogInformation("Adding [{0}] to Submission [{1}].", command.FilesToAdd.Count, toEdit.Id);
                    foreach (var file in command.FilesToAdd)
                    {
                        _logger.LogInformation("Attempting to add [{0}] to Directory Id: [{1}]", file.FileName, toEdit.ItemId);
                        var fileResult = await _fileStorageService.AddFileToDirectoryAsync(toEdit, file);
                        _logger.LogInformation("File [{0}] successfully added to Directory Id: [{1}] as Id: [{2}]", fileResult.ItemName, toEdit.ItemId, fileResult.ItemId);
                        _logger.LogInformation("Adding file to Submission Id: [{0}]", toEdit.Id);
                        toEdit.AddItem(fileResult);
                    }
                }
                // Remove any files
                if (command.FilesToRemove.Count > 0)
                {
                    _logger.LogInformation("Removing [{0}] from Submission [{1}].", command.FilesToRemove.Count, toEdit.Id);
                    foreach (int id in command.FilesToRemove)
                    {
                        _logger.LogInformation("Attempting to locate file id: [{0}] in Submission [{1}]'s file collection.", id, toEdit.Id);
                        var item = toEdit.Items.First(i => i.Id == id);
                        if (item != null)
                        {
                            _logger.LogInformation("File Id: [{0}] found, attemping to remove from storage via id: [{1}]", id, item.ItemId);
                            await _fileStorageService.DeleteFileAsync(item);
                            toEdit.RemoveItem(item);
                        }
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
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
