using AuthService.Helpers;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class RegistrationService : IRegistrationService<ApplicationUser>
    {
        private UserManager<ApplicationUser> _userManager;

        public RegistrationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> ExternalCreateAsync(string provider, string providerUserId, IEnumerable<Claim> claims)
        {
            ApplicationUser user;
            switch (provider)
            {
                case Const.ExternalProvider.Google:
                    user = CreateUserWithGoogleClaims(claims);
                    break;
                default:
                    return false;
            }
            var result = await _userManager.CreateAsync(user);
            return result.Succeeded;
        }

        private ApplicationUser CreateUserWithGoogleClaims(IEnumerable<Claim> claims)
        {
            var splitNameParts = claims.FirstOrDefault(x => x.Type == "name")?.Value.SplitString();
            string middleName = null;
            if (splitNameParts?.Count > 2)
                middleName = string.Join(" ", splitNameParts.GetRange(1, splitNameParts.Count - 2));
            return new ApplicationUser
            {
                ExternalProvider = Const.ExternalProvider.Google,
                ExternalProviderId = claims.FirstOrDefault(x => x.Type == "sub")?.Value,
                Email = claims.FirstOrDefault(x => x.Type == "email")?.Value,
                UserName = claims.FirstOrDefault(x => x.Type == "email")?.Value,
                FirstName = splitNameParts?[0],
                MiddleName = middleName,
                LastName = splitNameParts?[splitNameParts.Count - 1]
            };
        }
    }
}
