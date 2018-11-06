using AuthService.Common.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Common.Models
{
    public class ApplicationProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(subId);
            if (user == null)
                return;

            var claims = new List<Claim>
            {
                new Claim("username", user.UserName),
                new Claim("email", user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            var additionalClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(additionalClaims);

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(subId);
            context.IsActive = user != null;
        }
    }
}
