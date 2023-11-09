using AutoMapper;
using DocRouter.Application.Common.Exceptions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using DocRouter.Application.Submissions.Queries.GetSubmissionDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetSubmissionUsers
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{GetSubmissionDetailQuery, SubmissionDto}"/> that retrieves users associated with a submission.
    /// </summary>
    public class GetSubmissionUsersQueryHandler : IRequestHandler<GetSubmissionUsersQuery, List<DirectoryUser>>
    {
        private readonly IDocRouterContext _context;
        private readonly ILogger _logger;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        public GetSubmissionUsersQueryHandler(IDocRouterContext context, ILogger<GetSubmissionDetailQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetSubmissionUsersQuery"/></param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException">Thrown when no submission with the provided ID could be found.</exception>
        public async Task<List<DirectoryUser>> Handle(GetSubmissionUsersQuery request, CancellationToken cancellationToken)
        {
            // TODO: Inject a userService to reconcile email names with Display names?

            int? submissionId = await _context.Transactions
                .Where(x => x.Id == request.TransactionId)
                .Select(y => y.SubmissionId)
                .FirstOrDefaultAsync();
            if (submissionId == null)
            {
                _logger.LogError("No submission containing transaction with id: {0} was found.", request.TransactionId);
                throw new NotFoundException("No entity was found.", request.TransactionId);
            }
            var result = await _context.Submissions
                .Where(x => x.Id == submissionId)
                .Select(x => new DirectoryUser { Email = x.CreatedBy, Name = x.CreatedBy })
                .ToListAsync();
            if (result == null)
            {
                _logger.LogError("No submission with id: {0} was found.", submissionId);
                throw new NotFoundException("No entity with Id {0} was found.", submissionId);
            }
            return result;
        }
    }
}
