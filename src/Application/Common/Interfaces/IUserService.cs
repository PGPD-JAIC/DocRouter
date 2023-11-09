using DocRouter.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Interfaces
{
    /// <summary>
    /// Interface that describes a service to retrieve user information.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a list of Users.
        /// </summary>
        /// <returns></returns>
        Task<List<DirectoryUser>> GetAllUsersAsync();
        /// <summary>
        /// Retrieves a user with a given email address.
        /// </summary>
        /// <param name="email">A string containing the user's email address.</param>
        /// <returns>A <see cref="DirectoryUser"/> object containing the user's details. Will be empty if no user is found.</returns>
        Task<DirectoryUser> GetUserIdByEmail(string email);
        /// <summary>
        /// Gets a list of <see cref="Directory"/>s that the user with the given email can access.
        /// </summary>
        /// <param name="email">A string containing the user's email address.</param>
        /// <returns>A <see cref="List{Directory}"/> containing the directories that the user can access.</returns>
        Task<List<Directory>> GetUserDirectoriesByEmail(string email);
    }
}
