using DocRouter.Application.Common.Exceptions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Application.Submissions.Queries.GetCompleteTransactionDetail;
using DocRouter.Common;
using DocRouter.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.CompleteTransaction
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> that handles a request to complete a submission transaction.
    /// </summary>
    public class CompleteTransactionCommandHandler : IRequestHandler<CompleteTransactionCommand, Result>
    {
        private readonly IDocRouterContext _context;
        private readonly IDateTime _dateTime;
        private readonly ILogger<CompleteTransactionCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="mediator">An implementation of <see cref="IMediator"/></param>
        public CompleteTransactionCommandHandler(
            IDocRouterContext context,
            IDateTime dateTime,
            ILogger<CompleteTransactionCommandHandler> logger,
            IMediator mediator,
            ICurrentUserService currentUserService
            )
        {
            _context = context;
            _dateTime = dateTime;
            _logger = logger;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="command">A <see cref="CompleteTransactionCommand"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="Result"/></returns>
        public async Task<Result> Handle(CompleteTransactionCommand command, CancellationToken cancellationToken)
        {
            var submission = await _context.Submissions.FindAsync(command.SubmissionId);
            if (submission == null)
            {
                throw new NotFoundException($"No submission found with id: {command.SubmissionId}", command.SubmissionId);
            }
            var transactionToEdit = await _context.Transactions.FindAsync(command.TransactionId);
            if (transactionToEdit == null)
            {
                throw new NotFoundException($"No transaction found with id {command.TransactionId}", command.TransactionId);
            }
            transactionToEdit.UpdateTransactionStatus(DocRouter.Common.Enums.TransactionStatus.Approved);
            submission.AddTransaction(new SubmissionTransaction(
                _dateTime.Now,
                _dateTime.Now,
                DocRouter.Common.Enums.TransactionStatus.Complete,
                _currentUserService.UserId,
                command.NewComments
                ));
            await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new TransactionCompleted
            {
                SubmissionId = submission.Id,
                SubmissionUri = submission.FolderUri,
                SubmissionTitle = submission.Title,
                SubmittedBy = _currentUserService.UserId,
                SubmittedTo = command.Recepient
            });
            return Result.Success();
        }
    }
}
