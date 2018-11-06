using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Common.Interfaces.Services
{
    public interface IUserService<TUser>
        where TUser : class
    {
        Task<bool> ValidateCredentialsAsync(TUser user, string password);
        Task<TUser> FindByUsernameAsync(string userName);
        Task SignInAsync(TUser user, bool isPersistent);
        Task SignOutAsync();
        Task<TUser> FindUserByExternalProviderAsync(string provider, string providerUserId);
        Task<bool> CreateAsync(TUser user, string password);
        Task<bool> ExternalCreateAsync(string provider, string providerUserId, IEnumerable<Claim> claims);
    }
}
