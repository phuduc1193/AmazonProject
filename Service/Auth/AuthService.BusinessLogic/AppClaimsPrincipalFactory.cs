using AuthService.Common.Interfaces.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic
{
    public class AppClaimsPrincipalFactory<TUser, TRole> : UserClaimsPrincipalFactory<TUser, TRole>, IUserClaimsPrincipalFactory<TUser>
        where TUser : class, IApplicationUser
        where TRole : class, IApplicationRole
    {
        private UserManager<TUser> _userManager;

        public AppClaimsPrincipalFactory(UserManager<TUser> userManager, RoleManager<TRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
            _userManager = userManager;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var principal = await base.CreateAsync(user);

            var principalIdentity = (ClaimsIdentity)principal.Identity;
            principalIdentity.AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("username", user.UserName),
                new Claim("email", user.Email),
            });

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                principalIdentity.AddClaim(new Claim("role", role));
            }

            return principal;
        }
    }
}
