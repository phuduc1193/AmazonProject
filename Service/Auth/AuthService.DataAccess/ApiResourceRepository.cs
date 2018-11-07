using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Common.Interfaces.Contexts;
using AuthService.Common.Interfaces.Repositories;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess
{
    public class ApiResourceRepository : IApiResourceRepository
    {
        private ICustomConfigurationDbContext _context;

        public ApiResourceRepository(ICustomConfigurationDbContext context)
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

        public async Task<int> AddApiResourceAsync(ApiResource apiResource)
        {
            _context.ApiResources.Add(apiResource);
            var results = await _context.SaveChangesAsync();
            return results;
        }

        public async Task<int> UpdateApiResourceAsync(ApiResource apiResource)
        {
            _context.ApiResources.Update(apiResource);
            var scopesToDelete = (
                from scope in _context.ApiScopes
                where scope.ApiResource == apiResource
                    && !apiResource.Scopes.Contains(scope)
                select scope
            );
            _context.ApiScopes.RemoveRange(scopesToDelete);
            var secretsToDelete = (
                from secret in _context.ApiSecrets
                where secret.ApiResource == apiResource
                    && !apiResource.Secrets.Contains(secret)
                select secret
            );
            _context.ApiSecrets.RemoveRange(secretsToDelete);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> RemoveApiResourceByIdAsync(int id)
        {
            var apiResource = await GetApiResourceByIdAsync(id);
            _context.ApiScopes.RemoveRange(apiResource.Scopes);
            _context.ApiClaims.RemoveRange(apiResource.UserClaims);
            _context.ApiSecrets.RemoveRange(apiResource.Secrets);
            _context.ApiResources.Remove(apiResource);
            return await _context.SaveChangesAsync();
        }
    }
}
