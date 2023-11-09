namespace DocRouter.Application.Common.Interfaces
{
    /// <summary>
    /// Defines a service to retrieve information about the current user.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// The current User's Id
        /// </summary>
        string UserId { get; }
        
        /// <summary>
        /// Whether the current user is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }
        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; }
    }
}
