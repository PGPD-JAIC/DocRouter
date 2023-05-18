using DocRouter.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Interfaces
{
    /// <summary>
    /// Interface that defines an Authorizor contract.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAuthorizer<T>
    {
        /// <summary>
        /// Handles the authorization.
        /// </summary>
        /// <param name="instance">The request instance to authorize.</param>
        /// <param name="cancellation">A cancellation token.</param>
        /// <returns></returns>
        Task<AuthorizationResult> AuthorizeAsync(T instance, CancellationToken cancellation = default);
    }
}
