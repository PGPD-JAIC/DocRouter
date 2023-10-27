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

namespace DocRouter.Application.Submissions.Queries.GetEditSubmissionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{GetEditSubmissionDetailQuery, EditSubmissionCommand}"/> that handles a request to get submission details for editing.
    /// </summary>
    public class GetEditSubmissionDetailQueryHandler : IRequestHandler<GetEditSubmissionDetailQuery, EditSubmissionCommand>
    {
        private readonly IDocRouterContext _context;
        private readonly ILogger<GetEditSubmissionDetailQueryHandler> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"/></param>
        public GetEditSubmissionDetailQueryHandler(IDocRouterContext context, ILogger<GetEditSubmissionDetailQueryHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetEditSubmissionDetailQuery"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="RejectTransactionCommand"/> object.</returns>
        public async Task<EditSubmissionCommand> Handle(GetEditSubmissionDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Submissions
                .Where(x => x.Id == request.Id)
                .ProjectTo<EditSubmissionCommand>(_mapper.ConfigurationProvider)
                .SingleAsync();
            return vm;
        }
    }
}
