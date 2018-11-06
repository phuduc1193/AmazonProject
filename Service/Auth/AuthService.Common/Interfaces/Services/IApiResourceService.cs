using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Common.Interfaces.Services
{
    public interface IApiResourceService
    {
        Task<List<ApiResource>> GetListApiResourcesAsync();
        Task<bool> AddApiResourceAsync(ApiResource apiResource);
        Task<ApiResource> GetApiResourceByIdAsync(int id);
        Task<bool> UpdateApiResourceAsync(ApiResource apiResource);
    }
}
