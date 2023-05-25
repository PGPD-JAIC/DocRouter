using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Submissions.Commands.DeleteSubmission;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetDeleteSubmissionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler"/> that handles a request to get details of a submission for a deletion operation.
    /// </summary>
    public class GetDeleteSubmissionDetailQueryHandler : IRequestHandler<GetDeleteSubmissionDetailQuery, DeleteSubmissionCommand>
    {
        private readonly IDocRouterContext _context;
        private readonly ILogger<GetDeleteSubmissionDetailQueryHandler> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"/></param>
        public GetDeleteSubmissionDetailQueryHandler(IDocRouterContext context, ILogger<GetDeleteSubmissionDetailQueryHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetDeleteSubmissionDetailQuery"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="DeleteSubmissionCommand"/> object.</returns>
        public async Task<DeleteSubmissionCommand> Handle(GetDeleteSubmissionDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Submissions
                .Where(x => x.Id == request.SubmissionId)
                .ProjectTo<DeleteSubmissionCommand>(_mapper.ConfigurationProvider)
                .SingleAsync();
            return vm;
        }
    }
}
