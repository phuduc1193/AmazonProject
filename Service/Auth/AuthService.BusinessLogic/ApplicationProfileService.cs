using AuthService.Common.Interfaces.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic
{
    public class ApplicationProfileService<TUser> : IProfileService
        where TUser: class, IApplicationUser
    {
        private readonly ApplicationUserManager<TUser> _userManager;

        public ApplicationProfileService(ApplicationUserManager<TUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(subId);
            if (user == null)
                return;

            var claims = await _userManager.GetJwtClaimsAsync(user) as List<Claim>;
            context.IssuedClaims = FilterRequestedClaims(context.RequestedClaimTypes, claims);
        }

        private List<Claim> FilterRequestedClaims(IEnumerable<string> requestedClaimTypes, List<Claim> claims)
        {
            return claims.Where(c => requestedClaimTypes.Any(rc => string.Equals(rc, c.Type, StringComparison.InvariantCultureIgnoreCase))).ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(subId);
            context.IsActive = user != null;
        }
    }
}
