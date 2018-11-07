using AuthService.Common.Interfaces.Contexts;
using AuthService.Common.Interfaces.Repositories;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.DataAccess
{
    public class ClientRepository : IClientRepository
    {
        private IApplicationDbContext _context;

        public ClientRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetListClientsAsync()
        {
            var results = await _context.Clients
                .Include(x => x.AllowedScopes)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.Claims)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.ClientSecrets)
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.Properties)
                .ToListAsync();
            return results;
        }
    }
}
