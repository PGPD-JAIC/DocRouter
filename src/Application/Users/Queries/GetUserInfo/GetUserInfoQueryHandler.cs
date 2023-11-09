using DocRouter.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Users.Queries.GetUserInfo
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{GetUserInfoQuery, UserInfoVm}"/> that handles requests to retrieve user details.
    /// </summary>
    public class GetUserInfoQueryHandler: IRequestHandler<GetUserInfoQuery, UserInfoVm>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        private readonly ILogger<GetUserInfoQueryHandler>  _logger;
        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="currentUserService"></param>
        /// <param name="userService"></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        public GetUserInfoQueryHandler(ICurrentUserService currentUserService, IUserService userService, ILogger<GetUserInfoQueryHandler> logger)
        {
            _currentUserService = currentUserService;
            _userService = userService;
            _logger = logger;   
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetUserInfoQuery"/> object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="UserInfoVm"/> object.</returns>
        public async Task<UserInfoVm> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            UserInfoVm results = new UserInfoVm();
            try
            {
                var userInfo = await _userService.GetUserIdByEmail(_currentUserService.Email);
                results.DisplayName = userInfo.Name;
                results.Email = userInfo.Email;
                results.UserId = userInfo.Id;
                return results;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error retrieving user info: {0}", ex.Message);
                return results;
            }
        }
    }
}
