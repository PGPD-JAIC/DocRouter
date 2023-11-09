using DocRouter.Application.Common.Models;
using System.Collections.Generic;

namespace DocRouter.Application.Submissions.Queries.GetUserSubmissions
{
    /// <summary>
    /// Viewmodel class used to display a list of submissions to the current user.
    /// </summary>
    public class UserSubmissionListVm
    {
        /// <summary>
        /// A <see cref="ICollection{UserSubmissionListSubmissionDto}"/> containing submissions that have been routed to the current user.
        /// </summary>
        public ICollection<UserSubmissionListSubmissionDto> SubmittedTo { get; set; } = new List<UserSubmissionListSubmissionDto>();
        /// <summary>
        /// A <see cref="ICollection{UserSubmissionListSubmissionDto}"/> containing submissions that have been routed to the current user.
        /// </summary>
        public ICollection<UserSubmissionListSubmissionDto> SubmittedBy { get; set; } = new List<UserSubmissionListSubmissionDto>();
        /// <summary>
        /// The user's username.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Contains info used for paging the list.
        /// </summary>
        public PagingInfo SubmittedToPagingInfo { get; set; }
        /// <summary>
        /// Contains info used for paging the list.
        /// </summary>
        public PagingInfo SubmittedByPagingInfo { get; set; }
        /// <summary>
        /// An optional string to filter the submission lists to items with the provided name.
        /// </summary>
        public string TitleSearch { get; set; } = "";
    }
}
