using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Submissions.Queries.GetRejectTransactionDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetCompleteTransactionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> that handles a request to reject a submission transaction..
    /// </summary>
    public class GetCompleteTransactionDetailQueryHandler : IRequestHandler<GetCompleteTransactionDetailQuery, CompleteTransactionCommand>
    {
        private readonly IDocRouterContext _context;
        private readonly ILogger<GetCompleteTransactionDetailQueryHandler> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"/></param>
        /// <param name="userService">An implementation of <see cref="IUserService"/></param>
        public GetCompleteTransactionDetailQueryHandler(IDocRouterContext context, ILogger<GetCompleteTransactionDetailQueryHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetRejectTransactionDetailQuery"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="RejectTransactionCommand"/> object.</returns>
        public async Task<CompleteTransactionCommand> Handle(GetCompleteTransactionDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Transactions
                .Where(x => x.Id == request.TransactionId)
                .ProjectTo<CompleteTransactionCommand>(_mapper.ConfigurationProvider)
                .SingleAsync();
            return vm;
        }
    }
}
