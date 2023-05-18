using MediatR;
using System;

namespace DocRouter.Application.Submissions.Queries.GetAllSubmissions
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> used to retrieve a list of submissions.
    /// </summary>
    public class GetAllSubmissionsQuery : IRequest<SubmissionListVm>
    {
        /// <summary>
        /// A string containing the search filter used against the submittedby field.
        /// </summary>
        public string SubmittedBySearch { get; set; } = "";
        /// <summary>
        /// A string containing the search filter used against the routedTo field.
        /// </summary>
        public string RoutedToSearch { get; set; } = "";
        /// <summary>
        /// A datetime representing the start date of a date range search
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// A datetime representing the end date of a date range search
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// A string containing an optional sort order parameter.
        /// </summary>
        public string SortOrder { get; set; } = "";
        /// <summary>
        /// An integer page number. Defaults to 1.
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// An integer page size. Defaults to 25.
        /// </summary>
        public int PageSize { get; set; } = 25;
    }
}
