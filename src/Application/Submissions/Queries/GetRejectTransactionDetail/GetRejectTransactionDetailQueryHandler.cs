using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Submissions.Queries.GetApproveTransactionDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetRejectTransactionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler"/> that handles a request to get a transaction response.
    /// </summary>
    public class GetRejectTransactionDetailQueryHandler : IRequestHandler<GetRejectTransactionDetailQuery, RejectTransactionCommand>
    {
        private readonly IDocRouterContext _context;
        private readonly ILogger<GetApproveTransactionDetailQueryHandler> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"/></param>
        public GetRejectTransactionDetailQueryHandler(IDocRouterContext context, ILogger<GetApproveTransactionDetailQueryHandler> logger, IMapper mapper)
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
        public async Task<RejectTransactionCommand> Handle(GetRejectTransactionDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Transactions
                .Where(x => x.Id == request.TransactionId)
                .ProjectTo<RejectTransactionCommand>(_mapper.ConfigurationProvider)
                .SingleAsync();
            return vm;
        }
    }
}
