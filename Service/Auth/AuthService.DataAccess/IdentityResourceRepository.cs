using AuthService.Common.Interfaces.Contexts;
using AuthService.Common.Interfaces.Repositories;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.DataAccess
{
    public class IdentityResourceRepository : IIdentityResourceRepository
    {
        private IApplicationDbContext _context;

        public IdentityResourceRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddIdentityResourceAsync(IdentityResource identityResource)
        {
            _context.IdentityResources.Add(identityResource);
            var results = await _context.SaveChangesAsync();
            return results;
        }

        public async Task<IdentityResource> GetIdentityResourceByIdAsync(int id)
        {
            var result = await _context.IdentityResources
                 .Include(x => x.UserClaims)
                 .Where(x => x.Id == id)
                 .SingleOrDefaultAsync();
            return result;
        }

        public async Task<List<IdentityResource>> GetListIdentityResourcesAsync()
        {
            var results = await _context.IdentityResources
                    .Include(x => x.UserClaims)
                    .ToListAsync();
            return results;
        }

        public async Task<int> RemoveIdentityResourceByIdAsync(int id)
        {
            var identityResource = await GetIdentityResourceByIdAsync(id);
            _context.IdentityClaims.RemoveRange(identityResource.UserClaims);
            _context.IdentityResources.Remove(identityResource);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateIdentityResourceAsync(IdentityResource identityResource)
        {
            _context.IdentityResources.Update(identityResource);
            var claimsToDelete = (
                from claim in _context.IdentityClaims
                where claim.IdentityResource == identityResource
                    && (identityResource.UserClaims == null || !identityResource.UserClaims.Contains(claim))
                select claim
            );
            _context.IdentityClaims.RemoveRange(claimsToDelete);
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
