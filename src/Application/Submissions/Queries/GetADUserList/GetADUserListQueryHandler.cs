using DocRouter.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Submissions.Queries.GetADUserList
{
    public class GetADUserListQueryHandler : IRequestHandler<GetADUserListQuery, ADUserListVm>
    {
        private readonly IUserService _userService;

        public GetADUserListQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<ADUserListVm> Handle(GetADUserListQuery request, CancellationToken cancellationToken)
        {
            var vm = new ADUserListVm
            {
                Users = await _userService.GetAllUsersAsync()
            };
            return vm;
        }
    }
}
