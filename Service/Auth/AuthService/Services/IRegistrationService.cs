using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public interface IRegistrationService<TUser>
    {
        Task<bool> CreateAsync(TUser user, string password);
        Task<bool> ExternalCreateAsync(string provider, string providerUserId, IEnumerable<Claim> claims);
    }
}
