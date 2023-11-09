using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetAllSubmissions
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{GetAllSubmissionsQuery, SubmissionListVm}"/> that handles request to retrieve a list of submissions.
    /// </summary>
    public class GetAllSubmissionsQueryHandler : IRequestHandler<GetAllSubmissionsQuery, SubmissionListVm>
    {
        private readonly IDocRouterContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"/></param>
        public GetAllSubmissionsQueryHandler(IDocRouterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetAllSubmissionsQuery"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> object.</param>
        /// <returns></returns>
        public async Task<SubmissionListVm> Handle(GetAllSubmissionsQuery request, CancellationToken cancellationToken)
        {
            SubmissionListVm vm = new SubmissionListVm
            {
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = request.PageSize,
                    CurrentPage = request.PageNumber
                },
                RoutedToUsers = new List<string>(),
                SubmittingUsers = new List<string>(),
                Submissions = new List<SubmissionListSubmissionDto>(),
                SubmittedBySearch = request.SubmittedBySearch,
                RoutedToSearch = request.RoutedToSearch,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CurrentSort = request.SortOrder,
                DateSort = string.IsNullOrEmpty(request.SortOrder) ? "date_asc" : "",
                TitleSort = request.SortOrder == "title_desc" ? "title" : "title_desc",
                RoutedToSort = request.SortOrder == "routedTo_desc" ? "routedTo" : "routedTo",
                SubmittedBySort = request.SortOrder == "submittedBy_desc" ? "submittedBy" : "submittedBy_desc"
            };
            vm.RoutedToUsers = await _context.Transactions.Select(x => x.RoutedTo).Distinct().ToListAsync();
            vm.SubmittingUsers = await _context.Submissions.Select(x => x.CreatedBy).Distinct().ToListAsync();
            vm.Submissions = request.SortOrder switch
            {
                "date_asc" => await _context.Submissions.OrderBy(x => x.Created)
                    .Where(x => (request.StartDate == null || (x.Created >= request.StartDate && x.Created <= request.EndDate))
                    && (string.IsNullOrEmpty(request.SubmittedBySearch) || x.CreatedBy == request.SubmittedBySearch)
                    && (string.IsNullOrEmpty(request.RoutedToSearch) || x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.RoutedToSearch))
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<SubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),
                "title_desc" => await _context.Submissions.OrderByDescending(x => x.Title)
                    .Where(x => (request.StartDate == null || (x.Created >= request.StartDate && x.Created <= request.EndDate))
                    && (string.IsNullOrEmpty(request.SubmittedBySearch) || x.CreatedBy == request.SubmittedBySearch)
                    && (string.IsNullOrEmpty(request.RoutedToSearch) || x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.RoutedToSearch))
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<SubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),
                "title" => await _context.Submissions.OrderBy(x => x.Title)
                    .Where(x => (request.StartDate == null || (x.Created >= request.StartDate && x.Created <= request.EndDate))
                    && (string.IsNullOrEmpty(request.SubmittedBySearch) || x.CreatedBy == request.SubmittedBySearch)
                    && (string.IsNullOrEmpty(request.RoutedToSearch) || x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.RoutedToSearch))
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<SubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),
                "routedTo_desc" => await _context.Submissions.OrderByDescending(x => x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo)
                    .Where(x => (request.StartDate == null || (x.Created >= request.StartDate && x.Created <= request.EndDate))
                    && (string.IsNullOrEmpty(request.SubmittedBySearch) || x.CreatedBy == request.SubmittedBySearch)
                    && (string.IsNullOrEmpty(request.RoutedToSearch) || x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.RoutedToSearch))
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<SubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),
                "routedTo" => await _context.Submissions.OrderBy(x => x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo)
                    .Where(x => (request.StartDate == null || (x.Created >= request.StartDate && x.Created <= request.EndDate))
                    && (string.IsNullOrEmpty(request.SubmittedBySearch) || x.CreatedBy == request.SubmittedBySearch)
                    && (string.IsNullOrEmpty(request.RoutedToSearch) || x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.RoutedToSearch))
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<SubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),
                "submittedBy" => await _context.Submissions.OrderBy(x => x.CreatedBy)
                    .Where(x => (request.StartDate == null || (x.Created >= request.StartDate && x.Created <= request.EndDate))
                    && (string.IsNullOrEmpty(request.SubmittedBySearch) || x.CreatedBy == request.SubmittedBySearch)
                    && (string.IsNullOrEmpty(request.RoutedToSearch) || x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.RoutedToSearch))
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<SubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),
                "submittedBy_desc" => await _context.Submissions.OrderByDescending(x => x.CreatedBy)
                    .Where(x => (request.StartDate == null || (x.Created >= request.StartDate && x.Created <= request.EndDate))
                    && (string.IsNullOrEmpty(request.SubmittedBySearch) || x.CreatedBy == request.SubmittedBySearch)
                    && (string.IsNullOrEmpty(request.RoutedToSearch) || x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.RoutedToSearch))
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<SubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),
                _ => await _context.Submissions.OrderBy(x => x.Created)
                    .Where(x => (request.StartDate == null || (x.Created >= request.StartDate && x.Created <= request.EndDate))
                    && (string.IsNullOrEmpty(request.SubmittedBySearch) || x.CreatedBy == request.SubmittedBySearch)
                    && (string.IsNullOrEmpty(request.RoutedToSearch) || x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.RoutedToSearch))
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<SubmissionListSubmissionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
            vm.PagingInfo.TotalItems = await _context.Submissions
                    .Where(x => (request.StartDate == null || (x.Created >= request.StartDate && x.Created <= request.EndDate))
                    && (string.IsNullOrEmpty(request.SubmittedBySearch) || x.CreatedBy == request.SubmittedBySearch)
                    && (string.IsNullOrEmpty(request.RoutedToSearch) || x.Transactions.OrderByDescending(y => y.Created).First().RoutedTo == request.RoutedToSearch))
                    .CountAsync(cancellationToken);
            return vm;
        }
    }
}
