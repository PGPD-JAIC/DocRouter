using DocRouter.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Users.Queries.GetUserDirectories
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{GetUserDirectoryListQuery, UserDirectoryListVm}"/> that handles requests to retrieve a list of directories available to the current user.
    /// </summary>
    public class GetUserDirectoryListQueryHandler : IRequestHandler<GetUserDirectoryListQuery, UserDirectoryListVm>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="currentUserService">An implementation of <see cref="ICurrentUserService"></see></param>
        /// <param name="userService">An implementation of <see cref="IUserService"/></param>
        public GetUserDirectoryListQueryHandler(ICurrentUserService currentUserService, IUserService userService) 
        { 
            _currentUserService = currentUserService;
            _userService = userService;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetUserDirectoryListQuery"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="UserDirectoryListVm"/></returns>
        public async Task<UserDirectoryListVm> Handle(GetUserDirectoryListQuery request, CancellationToken cancellationToken)
        {
            var vm = new UserDirectoryListVm
            {
                Directories = await _userService.GetUserDirectoriesByEmail(_currentUserService.Email)
            };
            return vm;
        }
    }
}
