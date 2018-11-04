using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Data;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class ApiResourceService : IApiResourceService
    {
        private CustomConfigurationDbContext _context;

        public ApiResourceService(CustomConfigurationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResource> GetApiResourceByIdAsync(int id)
        {
            var result = await _context.ApiResources
                .Include(x => x.Scopes)
                .Include(x => x.Secrets)
                .Include(x => x.UserClaims)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            return result;
        }

        public async Task<List<ApiResource>> GetListApiResourcesAsync()
        {
            var results = await _context.ApiResources
                .Include(x => x.Scopes)
                .Include(x => x.Secrets)
                .Include(x => x.UserClaims)
                .ToListAsync();
            return results;
        }

        public async Task<bool> AddApiResourceAsync(ApiResource apiResource)
        {
            _context.ApiResources.Add(apiResource);
            var results = await _context.SaveChangesAsync();
            return results > 0;
        }

        public async Task<bool> UpdateApiResourceAsync(ApiResource apiResource)
        {
            try
            {
                _context.ApiResources.Update(apiResource);
                var scopesToDelete = (
                    from scope in _context.ApiScopes
                    where scope.ApiResource == apiResource && !apiResource.Scopes.Contains(scope)
                    select scope
                );
                _context.ApiScopes.RemoveRange(scopesToDelete);
                var result = await _context.SaveChangesAsync();
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
