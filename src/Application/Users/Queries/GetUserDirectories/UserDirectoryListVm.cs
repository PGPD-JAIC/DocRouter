using DocRouter.Application.Common.Models;
using System.Collections.Generic;


namespace DocRouter.Application.Users.Queries.GetUserDirectories
{
    /// <summary>
    /// Viewmodel class used to return a list of <see cref="Directory"/> objects.
    /// </summary>
    public class UserDirectoryListVm
    {
        /// <summary>
        /// A list of <see cref="Directory"/> objects.
        /// </summary>
        public List<Directory> Directories { get; set; }
    }
}