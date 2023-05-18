using DocRouter.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DocRouter.WebUI.Services
{
    public class ClaimsLoader : IClaimsTransformation
    {
        private readonly IDocRouterContext _context;

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IDocRouterContext"/></param>
        public ClaimsLoader(IDocRouterContext context)
        {
            _context = context;
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            //var identity = principal.Identities.FirstOrDefault(x => x.IsAuthenticated);
            //if (identity == null) return principal;
            //var user = identity.Name;
            //if (user == null) return principal;
            //if (principal.Identity is ClaimsIdentity identity1)
            //{
            //    string logonName = user.Split('\\')[1];
            //    List<UserRole> userRoles = await _context.UserRoles
            //        .Include(x => x.Role)
            //        .Where(x => x.UserName == user)
            //        .ToListAsync();
            //    var ci = identity1;
            //    foreach (UserRole userRole in userRoles)
            //    {
            //        var c = new Claim(ci.RoleClaimType, userRole.Role.RoleTypeName);
            //        ci.AddClaim(c);
            //    }
            //}
            return principal;
        }
    }
}
