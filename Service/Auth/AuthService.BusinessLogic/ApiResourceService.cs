using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Common.Interfaces.Services;
using AuthService.Common.Interfaces.Repositories;
using IdentityServer4.EntityFramework.Entities;

namespace AuthService.BusinessLogic
{
    public class ApiResourceService : IApiResourceService
    {
        private IApiResourceRepository _repo;

        public ApiResourceService(IApiResourceRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResource> GetApiResourceByIdAsync(int id)
        {
            var result = await _repo.GetApiResourceByIdAsync(id);
            return result;
        }

        public async Task<List<ApiResource>> GetListApiResourcesAsync()
        {
            var results = await _repo.GetListApiResourcesAsync();
            return results;
        }

        public async Task<bool> AddApiResourceAsync(ApiResource apiResource)
        {
            var results = await _repo.AddApiResourceAsync(apiResource);
            return results > 0;
        }

        public async Task<bool> UpdateApiResourceAsync(ApiResource apiResource)
        {
            try
            {
                var result = await _repo.UpdateApiResourceAsync(apiResource);
                return true;
            }
            catch
            {
                //TODO: Log
                return false;
            }
        }
    }
}
