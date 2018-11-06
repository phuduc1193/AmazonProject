using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Common.Interfaces.Services;
using AuthService.Common.Interfaces.Repositories;
using IdentityServer4.EntityFramework.Entities;
using System.Linq;

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
            var results = await _repo.AddApiResourceAsync(AddScopeIfEmpty(apiResource));
            return results > 0;
        }

        public async Task<bool> UpdateApiResourceAsync(ApiResource apiResource)
        {
            try
            {
                var result = await _repo.UpdateApiResourceAsync(AddScopeIfEmpty(apiResource));
                return true;
            }
            catch
            {
                //TODO: Log
                return false;
            }
        }

        private static ApiResource AddScopeIfEmpty(ApiResource apiResource)
        {
            if (apiResource.Scopes == null || apiResource.Scopes.Count == 0
                || string.IsNullOrWhiteSpace(apiResource.Scopes.First().Name))
            {
                apiResource.Scopes = new List<ApiScope> {
                    new ApiScope {
                        Name = apiResource.Name,
                        DisplayName = apiResource.DisplayName,
                        Description = apiResource.Description,
                        Emphasize = true,
                        Required = true
                    }
                };
            }
            return apiResource;
        }

        public async Task<bool> RemoveApiResourceByIdAsync(int id)
        {
            try
            {
                var result = await _repo.RemoveApiResourceByIdAsync(id);
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
