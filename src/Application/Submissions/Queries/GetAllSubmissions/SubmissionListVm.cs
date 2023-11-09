using DocRouter.Application.Common.Models;
using System;
using System.Collections.Generic;

namespace DocRouter.Application.Submissions.Queries.GetAllSubmissions
{
    /// <summary>
    /// Viewmodel class used to return the results of a <see cref="GetAllSubmissionsQuery"/>
    /// </summary>
    public class SubmissionListVm
    {   
        /// <summary>
        /// A list of <see cref="SubmissionListSubmissionDto"/>
        /// </summary>
        public ICollection<SubmissionListSubmissionDto> Submissions { get; set; }
        /// <summary>
        /// A list of user names.
        /// </summary>
        public List<string> SubmittingUsers { get; set; }
        /// <summary>
        /// A list of user names.
        /// </summary>
        public List<string> RoutedToUsers { get; set; }
        /// <summary>
        /// A string containing the search filter used against the submittedby field.
        /// </summary>
        public string SubmittedBySearch { get; set; }
        /// <summary>
        /// A string containing the search filter used against the routedTo field.
        /// </summary>
        public string RoutedToSearch { get; set; }
        /// <summary>
        /// A datetime representing the start date of a date range search
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// A datetime representing the end date of a date range search
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Contains info used for paging the list.
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
        /// <summary>
        /// Contains the current sorting value being applied to the list.
        /// </summary>
        public string CurrentSort { get; set; }
        /// <summary>
        /// Contains a string sorting value against the Date Field
        /// </summary>
        public string DateSort { get; set; }
        /// <summary>
        /// Contains a string sorting value against the Title Field.
        /// </summary>
        public string TitleSort { get; set; }
        /// <summary>
        /// Contains a string sorting value against the RoutedTo field.
        /// </summary>
        public string RoutedToSort { get; set; }
        /// <summary>
        /// Contains a string sorting value against the SubmittedBy field.
        /// </summary>
        public string SubmittedBySort { get; set; }
    }
}
