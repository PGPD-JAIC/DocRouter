using DocRouter.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DocRouter.WebUI.Services
{
    /// <summary>
    /// Implementation of <see cref="ICurrentUserService"/> that serves detais for the current user.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="httpContextAccessor">An implementation of <see cref="IHttpContextAccessor"/></param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.Identity.Name;
            IsAuthenticated = UserId != null;
        }
        /// <summary>
        /// The User name of the current user.
        /// </summary>
        public string UserId { get; }
        /// <summary>
        /// Indicates whether the user is authenticated.
        /// </summary>
        public bool IsAuthenticated { get; }
    }
}
