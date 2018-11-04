using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public interface IApiResourceService
    {
        Task<List<ApiResource>> GetListApiResourcesAsync();
        Task<bool> SaveApiResourceAsync(ApiResource apiResource);
    }
}
