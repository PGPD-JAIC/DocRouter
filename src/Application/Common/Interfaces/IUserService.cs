using DocRouter.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocRouter.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<List<DirectoryUser>> GetAllUsersAsync();
    }
}
