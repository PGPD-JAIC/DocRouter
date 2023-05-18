using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using System.Threading.Tasks;

namespace DocRouter.Application.UnitTests.Common
{
    public class TestNotificationService : INotificationService
    {
        public Task SendAsync(MessageDto message)
        {
            return Task.CompletedTask;
        }
    }
}
