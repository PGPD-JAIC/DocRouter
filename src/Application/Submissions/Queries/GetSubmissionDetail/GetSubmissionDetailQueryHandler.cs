using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocRouter.Application.Common.Exceptions;
using DocRouter.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetSubmissionDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{GetSubmissionDetailQuery, SubmissionDto}"/> that retrieves details of a submission.
    /// </summary>
    public class GetSubmissionDetailQueryHandler : IRequestHandler<GetSubmissionDetailQuery, SubmissionDto>
    {
        private readonly IDocRouterContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"/></param>
        public GetSubmissionDetailQueryHandler(IDocRouterContext context, ILogger<GetSubmissionDetailQueryHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetSubmissionDetailQuery"/></param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException">Thrown when no submission with the provided ID could be found.</exception>
        public async Task<SubmissionDto> Handle(GetSubmissionDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Submissions
                .ProjectTo<SubmissionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (result == null)
            {
                _logger.LogError("GetSubmissionDetail failed: no submission with id: {0} found.", request.Id);
                throw new NotFoundException($"No entity with Id {request.Id} was found.", request.Id);
            }
            return result;
        }
    }
}
