using DocRouter.Application.Common.Exceptions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Application.Submissions.Queries.GetRejectTransactionDetail;
using DocRouter.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Commands.RejectTransaction
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> that handles a request to reject a submission transaction..
    /// </summary>
    public class RejectTransactionCommandHandler : IRequestHandler<RejectTransactionCommand, Result>
    {
        private readonly IDocRouterContext _context;
        private readonly IDateTime _dateTime;
        private readonly ILogger<RejectTransactionCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;


        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="mediator">An implementation of <see cref="IMediator"/></param>
        public RejectTransactionCommandHandler(
            IDocRouterContext context,
            IDateTime dateTime,
            ILogger<RejectTransactionCommandHandler> logger,
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

        public async Task<Result> Handle(RejectTransactionCommand command, CancellationToken cancellationToken)
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
        }
    }
}
