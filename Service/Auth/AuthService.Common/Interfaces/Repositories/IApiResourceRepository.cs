using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Common.Interfaces.Repositories
{
    public interface IApiResourceRepository
    {
        Task<List<ApiResource>> GetListApiResourcesAsync();
        Task<int> AddApiResourceAsync(ApiResource apiResource);
        Task<ApiResource> GetApiResourceByIdAsync(int id);
        Task<int> UpdateApiResourceAsync(ApiResource apiResource);
        Task<int> RemoveApiResourceByIdAsync(int id);
    }
}
