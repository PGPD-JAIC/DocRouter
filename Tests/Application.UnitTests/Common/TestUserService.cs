using DocRouter.Application.Common.Interfaces;
using DocRouter.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocRouter.Application.UnitTests.Common
{
    public class TestUserService : IUserService
    {
        public Task<List<DirectoryUser>> GetAllUsersAsync()
        {
            // TODO: Implement a real version of ADUserService to retrieve email addresses.
            return Task.FromResult(new List<DirectoryUser>() {
                new DirectoryUser
                {
                    Name = "Sgt. J. Smith #3134",
                    Email = "jcsmith1@co.pg.md.us"
                },
                new DirectoryUser
                {
                    Name = "Lt. J. Smith #3134",
                    Email = "jcsmith1@co.pg.md.us"
                },
                new DirectoryUser
                {
                    Name = "Capt. J. Smith #3134",
                    Email = "jcsmith1@co.pg.md.us"
                }
            });
        }
    }
}
