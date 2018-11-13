using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Common.Interfaces.Services
{
    public interface IIdentityResourceService
    {
        Task<List<IdentityResource>> GetListIdentityResourcesAsync();
        Task<bool> AddIdentityResourceAsync(IdentityResource identityResource);
        Task<IdentityResource> GetIdentityResourceByIdAsync(int id);
        Task<bool> UpdateIdentityResourceAsync(IdentityResource identityResource);
        Task<bool> RemoveIdentityResourceByIdAsync(int id);
    }
}
