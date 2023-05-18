using System;

namespace DocRouter.Application.Common.Models
{
    /// <summary>
    /// Class used in List View Queries to control Paging
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// Total items in the list
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// Items per page
        /// </summary>
        public int ItemsPerPage { get; set; }
        /// <summary>
        /// Current Page
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total pages containing items
        /// </summary>
        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        public string Caption => $"Showing {((CurrentPage - 1) * ItemsPerPage) + 1} to {(TotalItems < (CurrentPage * ItemsPerPage) ? TotalItems : (CurrentPage * ItemsPerPage))} of {TotalItems} Items.";

    }
}
