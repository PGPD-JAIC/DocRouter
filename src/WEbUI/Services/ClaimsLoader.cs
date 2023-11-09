using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DocRouter.WebUI.Services
{
    /// <summary>
    /// Implementation of <see cref="IClaimsTransformation"/> that loads user claims.
    /// </summary>
    public class ClaimsLoader : IClaimsTransformation
    {
        private List<string> _adminUsers;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="configuration">A <see cref="IConfiguration"/> obtained from the container.</param>
        public ClaimsLoader(IConfiguration configuration)
        {
            _adminUsers = configuration.GetSection("AdminUsers").Get<List<string>>();
        }
        /// <summary>
        /// Transforms the claims.
        /// </summary>
        /// <remarks>
        /// This method will add an "Admin" claim to any usernames in the "AdminUsers" list in configuration.
        /// </remarks>
        /// <param name="principal"></param>
        /// <returns>A <see cref="Task{ClaimsPrincipal}"/> object</returns>
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identities.FirstOrDefault(x => x.IsAuthenticated);
            if (identity == null) return principal;
            var user = identity.Name;
            if (user == null) return principal;
            if (principal.Identity is ClaimsIdentity identity1)
            {
                string logonName = user.Split('\\')[1];
                var ci = identity1;
                if (_adminUsers.Contains(logonName))
                {
                    var c = new Claim(ci.RoleClaimType, "Admin");
                    ci.AddClaim(c);
                }
            }
            return principal;
        }
    }
}
