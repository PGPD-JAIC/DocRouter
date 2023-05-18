using DocRouter.Application.Common.Models;
using System.Collections.Generic;

namespace DocRouter.Application.Submissions.Queries.GetADUserList
{
    public class ADUserListVm
    {
        public List<DirectoryUser> Users { get; set; }
    }
}
