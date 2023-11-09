using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocRouter.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetUserSubmissions
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{GetUserSubmissionsQuery, UserSubmissionListVm}"/> that handles request to retrieve a list of submissions.
    /// </summary>
    public class GetUserSubmissionsQueryHandler : IRequestHandler<GetUserSubmissionsQuery, UserSubmissionListVm>
    {
        private readonly IDocRouterContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"/></param>
        /// <param name="currentUserService">An implementation of <see cref="ICurrentUserService"/></param>
        public GetUserSubmissionsQueryHandler(IDocRouterContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetUserSubmissionsQuery"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="UserSubmissionListVm"/> containing the query results.</returns>
        public async Task<UserSubmissionListVm> Handle(GetUserSubmissionsQuery request, CancellationToken cancellationToken)
        {
            UserSubmissionListVm vm = new UserSubmissionListVm
            {
                SubmittedToPagingInfo = new Common.Models.PagingInfo
                {
                    ItemsPerPage = request.SubmittedToPageSize,
                    CurrentPage = request.SubmittedToPageNumber,
                    ModelModifier = "submittedTo"
                },
                SubmittedByPagingInfo = new Common.Models.PagingInfo
                {
                    ItemsPerPage = request.SubmittedByPageSize,
                    CurrentPage = request.SubmittedByPageNumber,
                    ModelModifier = "submittedBy"
                },
                TitleSearch = request.TitleSearch,
                SubmittedTo = await _context.Submissions.OrderByDescending(x => x.Created)
                .Where(x => (string.IsNullOrEmpty(request.TitleSearch) || EF.Functions.Like(x.Title, $"%{request.TitleSearch}%"))
                && x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.UserName)
                .Skip((request.SubmittedToPageNumber - 1) * request.SubmittedToPageSize)
                .Take(request.SubmittedToPageSize)
                .ProjectTo<UserSubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
            };

            vm.SubmittedToPagingInfo.TotalItems = await _context.Submissions
                .Where(x => (string.IsNullOrEmpty(request.TitleSearch) || EF.Functions.Like(x.Title, $"%{request.TitleSearch}%"))
                && x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.UserName)
                .CountAsync(cancellationToken);

            vm.SubmittedBy = await _context.Submissions.OrderByDescending(x => x.Created)
                .Where(x => (string.IsNullOrEmpty(request.TitleSearch) || EF.Functions.Like(x.Title, $"%{request.TitleSearch}%"))
                && x.CreatedBy == request.UserName)
                .Skip((request.SubmittedByPageNumber - 1) * request.SubmittedByPageSize)
                .Take(request.SubmittedByPageSize)
                .ProjectTo<UserSubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            vm.SubmittedByPagingInfo.TotalItems = await _context.Submissions
                .Where(x => (string.IsNullOrEmpty(request.TitleSearch) || EF.Functions.Like(x.Title, $"%{request.TitleSearch}%"))
                && x.CreatedBy == request.UserName)
                .CountAsync(cancellationToken);

            return vm;
        }
    }
}
