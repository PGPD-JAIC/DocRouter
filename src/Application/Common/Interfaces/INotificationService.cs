using DocRouter.Application.Common.Models;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(MessageDto message);
    }
}
