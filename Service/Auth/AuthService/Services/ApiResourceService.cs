using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class ApiResourceService : IApiResourceService
    {
        private IConfigurationDbContext _context;

        public ApiResourceService(IConfigurationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApiResource>> GetListApiResourcesAsync()
        {
            return await _context.ApiResources.ToListAsync();
        }

        public async Task<bool> SaveApiResourceAsync(ApiResource apiResource)
        {
            _context.ApiResources.Add(apiResource);
            var results = await _context.SaveChangesAsync();
            return results > 0;
        }
    }
}
