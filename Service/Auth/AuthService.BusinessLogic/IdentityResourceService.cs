using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Common.Interfaces.Services;
using AuthService.Common.Interfaces.Repositories;
using IdentityServer4.EntityFramework.Entities;
using System.Linq;

namespace AuthService.BusinessLogic
{
    public class IdentityResourceService : IIdentityResourceService
    {
        private IIdentityResourceRepository _repo;

        public IdentityResourceService(IIdentityResourceRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<IdentityResource>> GetListIdentityResourcesAsync()
        {
            var results = await _repo.GetListIdentityResourcesAsync();
            return results;
        }

        public async Task<bool> AddIdentityResourceAsync(IdentityResource identityResource)
        {
            var result = await _repo.AddIdentityResourceAsync(identityResource);
            return result > 0;
        }

        public async Task<IdentityResource> GetIdentityResourceByIdAsync(int id)
        {
            var result = await _repo.GetIdentityResourceByIdAsync(id);
            return result;
        }

        public async Task<bool> UpdateIdentityResourceAsync(IdentityResource identityResource)
        {
            var result = await _repo.UpdateIdentityResourceAsync(identityResource);
            return result > 0;
        }

        public async Task<bool> RemoveIdentityResourceByIdAsync(int id)
        {
            var result = await _repo.RemoveIdentityResourceByIdAsync(id);
            return result > 0;
        }
    }
}
