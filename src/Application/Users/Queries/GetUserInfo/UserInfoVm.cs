namespace DocRouter.Application.Users.Queries.GetUserInfo
{
    /// <summary>
    /// Viewmodel class that is used to return details of a User.
    /// </summary>
    public class UserInfoVm
    {
        /// <summary>
        /// The User's display name.
        /// </summary>
        public string DisplayName { get; set; } = "Guest";
        /// <summary>
        /// The User's email address.
        /// </summary>
        public string Email { get; set; } = "N/A";
        /// <summary>
        /// The user's ID.
        /// </summary>
        public string UserId { get; set; } = "N/A";
    }
}
