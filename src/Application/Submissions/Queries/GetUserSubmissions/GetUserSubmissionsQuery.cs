using MediatR;

namespace DocRouter.Application.Submissions.Queries.GetUserSubmissions
{
    /// <summary>
    /// Implementation of <see cref="IRequest"/> that retrieves a list of the user's submissions.
    /// </summary>
    public class GetUserSubmissionsQuery : IRequest<UserSubmissionListVm>
    {
        /// <summary>
        /// An optional string to filter the lists to submissions with the provided name.
        /// </summary>
        public string TitleSearch { get; set; } = "";
        /// <summary>
        /// An optional string containing a username.
        /// </summary>
        /// <remarks>
        /// This will filter the results list to show only submissions created by the user with this username.
        /// </remarks>
        public string CreatedBy { get; set; } = "";
        /// <summary>
        /// A string containing the current user's username.
        /// </summary>
        public string UserName { get; set; } = "";
        /// <summary>
        /// An integer page number. Defaults to 1.
        /// </summary>
        public int SubmittedByPageNumber { get; set; } = 1;
        /// <summary>
        /// An integer page size. Defaults to 25.
        /// </summary>
        public int SubmittedByPageSize { get; set; } = 25;
        /// <summary>
        /// An integer page number. Defaults to 1.
        /// </summary>
        public int SubmittedToPageNumber { get; set; } = 1;
        /// <summary>
        /// An integer page size. Defaults to 25.
        /// </summary>
        public int SubmittedToPageSize { get; set; } = 25;
    }
}
