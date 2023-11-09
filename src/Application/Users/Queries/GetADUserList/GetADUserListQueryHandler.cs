using DocRouter.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Users.Queries.GetADUserList
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{GetADUserListQuery, ADUserListVm}"/> that handles request to retrieve a list of users.
    /// </summary>
    public class GetADUserListQueryHandler : IRequestHandler<GetADUserListQuery, ADUserListVm>
    {
        private readonly IUserService _userService;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="userService">An implementation of <see cref="IUserService"/></param>
        public GetADUserListQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetADUserListQuery"></see> object.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
