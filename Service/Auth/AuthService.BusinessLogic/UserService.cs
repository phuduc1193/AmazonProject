using AuthService.Common.Helpers;
using AuthService.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService.Common.Interfaces.Services;
using AuthService.Common;
using AuthService.Common.Interfaces.Models;

namespace AuthService.BusinessLogic
{
    public class UserService<TUser> : IUserService<TUser>
        where TUser : class, IApplicationUser
    {
        private UserManager<TUser> _userManager;
        private SignInManager<TUser> _signInManager;

        public UserService(UserManager<TUser> userManager, SignInManager<TUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<TUser> FindByUsernameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<TUser> FindUserByExternalProviderAsync(string provider, string providerUserId)
        {
            return await _userManager.FindByLoginAsync(provider, providerUserId);
        }

        public Task SignInAsync(TUser user, bool isPersistent = true)
        {
            return _signInManager.SignInAsync(user, isPersistent);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<bool> ValidateCredentialsAsync(TUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> CreateAsync(TUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> ExternalCreateAsync(string provider, string providerUserId, IEnumerable<Claim> claims)
        {
            TUser user;
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

        private TUser CreateUserWithGoogleClaims(IEnumerable<Claim> claims)
        {
            var splitNameParts = claims.FirstOrDefault(x => x.Type == "name")?.Value.SplitString();
            string middleName = null;
            if (splitNameParts?.Count > 2)
                middleName = string.Join(" ", splitNameParts.GetRange(1, splitNameParts.Count - 2));
            var user = new ApplicationUser
            {
                ExternalProvider = Const.ExternalProvider.Google,
                ExternalProviderId = claims.FirstOrDefault(x => x.Type == "sub")?.Value,
                Email = claims.FirstOrDefault(x => x.Type == "email")?.Value,
                UserName = claims.FirstOrDefault(x => x.Type == "email")?.Value,
                FirstName = splitNameParts?[0],
                MiddleName = middleName,
                LastName = splitNameParts?[splitNameParts.Count - 1]
            };
            return user as TUser;
        }
    }
}
