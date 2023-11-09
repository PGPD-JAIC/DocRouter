using DocRouter.Application.Common.Interfaces;

namespace DocRouter.Application.UnitTests.Common
{
    public class CurrentUserServiceTesting : ICurrentUserService
    {
        public CurrentUserServiceTesting()
        {
            UserId = "user123";
            IsAuthenticated = true;
        }
        public CurrentUserServiceTesting(string LDAPName, bool isAuthenticated)
        {
            UserId = LDAPName;
            IsAuthenticated = true;
            Email = LDAPName + "@TEST.co.pg.md.us";
        }
        public string UserId { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string Email { get; private set; }
    }
}
