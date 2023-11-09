using DocRouter.Application.Common.Models;
using System.Collections.Generic;

namespace DocRouter.Application.Users.Queries.GetADUserList
{
    /// <summary>
    /// Viewmodel class used to display a list of <see cref="DirectoryUser"/> objects representing users.
    /// </summary>
    public class ADUserListVm
    {
        /// <summary>
        /// A list of users mapped into <see cref="DirectoryUser"/> objects.
        /// </summary>
        public List<DirectoryUser> Users { get; set; }
    }
}
