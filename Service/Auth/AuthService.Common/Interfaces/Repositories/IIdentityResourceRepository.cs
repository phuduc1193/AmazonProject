using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Common.Interfaces.Repositories
{
    public interface IIdentityResourceRepository
    {
        Task<List<IdentityResource>> GetListIdentityResourcesAsync();
        Task<int> AddIdentityResourceAsync(IdentityResource identityResource);
        Task<IdentityResource> GetIdentityResourceByIdAsync(int id);
        Task<int> UpdateIdentityResourceAsync(IdentityResource identityResource);
        Task<int> RemoveIdentityResourceByIdAsync(int id);
    }
}
