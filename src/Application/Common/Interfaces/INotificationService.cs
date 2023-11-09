using DocRouter.Application.Common.Models;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Interfaces
{
    /// <summary>
    /// Interface that defines a messaging service.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="message">A <see cref="MessageDto"/> object containing the details of the message.</param>
        /// <returns></returns>
        Task SendAsync(MessageDto message);
    }
}
