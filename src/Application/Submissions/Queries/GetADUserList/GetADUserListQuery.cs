using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocRouter.Application.Submissions.Queries.GetADUserList
{
    public class GetADUserListQuery : IRequest<ADUserListVm>
    {
    }
}
