using AuthService.Common.Interfaces.Contexts;
using AuthService.Common.Interfaces.Repositories;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> AddClientAsync(Client client)
        {
            _context.Clients.Add(client);
            var result = await _context.SaveChangesAsync();
            return result;
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

        public async Task<int> RemoveClientByIdAsync(int id)
        {
            var client = await GetClientByIdAsync(id);
            _context.Clients.Remove(client);
            _context.ClientScopes.RemoveRange(client.AllowedScopes);
            _context.ClientIdPRestrictions.RemoveRange(client.IdentityProviderRestrictions);
            _context.ClientClaims.RemoveRange(client.Claims);
            _context.ClientCorsOrigins.RemoveRange(client.AllowedCorsOrigins);
            _context.ClientSecrets.RemoveRange(client.ClientSecrets);
            _context.ClientGrantTypes.RemoveRange(client.AllowedGrantTypes);
            _context.ClientRedirectUris.RemoveRange(client.RedirectUris);
            _context.ClientPostLogoutRedirectUris.RemoveRange(client.PostLogoutRedirectUris);
            _context.ClientProperties.RemoveRange(client.Properties);

            return await _context.SaveChangesAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var result = await _context.Clients
                            .Include(x => x.AllowedScopes)
                            .Include(x => x.IdentityProviderRestrictions)
                            .Include(x => x.Claims)
                            .Include(x => x.AllowedCorsOrigins)
                            .Include(x => x.ClientSecrets)
                            .Include(x => x.AllowedGrantTypes)
                            .Include(x => x.RedirectUris)
                            .Include(x => x.PostLogoutRedirectUris)
                            .Include(x => x.Properties)
                            .Where(x => x.Id == id)
                            .SingleOrDefaultAsync();
            return result;
        }

        public async Task<int> UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            var scopesToDelete = (
                from scope in _context.ClientScopes
                where scope.Client == client
                    && (client.AllowedScopes == null || !client.AllowedScopes.Contains(scope))
                select scope
            );
            _context.ClientScopes.RemoveRange(scopesToDelete);
            var idPRestrictionsToDelete = (
                from restriction in _context.ClientIdPRestrictions
                where restriction.Client == client
                    && (client.IdentityProviderRestrictions == null || !client.IdentityProviderRestrictions.Contains(restriction))
                select restriction
            );
            _context.ClientIdPRestrictions.RemoveRange(idPRestrictionsToDelete);
            var claimsToDelete = (
                from claim in _context.ClientClaims
                where claim.Client == client
                    && (client.Claims == null || !client.Claims.Contains(claim))
                select claim
            );
            _context.ClientClaims.RemoveRange(claimsToDelete);
            var corsOriginsToDelete = (
                from cors in _context.ClientCorsOrigins
                where cors.Client == client
                    && (client.AllowedCorsOrigins == null || !client.AllowedCorsOrigins.Contains(cors))
                select cors
            );
            _context.ClientCorsOrigins.RemoveRange(corsOriginsToDelete);
            var secretsToDelete = (
                from secret in _context.ClientSecrets
                where secret.Client == client
                    && (client.ClientSecrets == null || !client.ClientSecrets.Contains(secret))
                select secret
            );
            _context.ClientSecrets.RemoveRange(secretsToDelete);
            var grantsToDelete = (
                from grant in _context.ClientGrantTypes
                where grant.Client == client
                    && (client.AllowedGrantTypes == null || !client.AllowedGrantTypes.Contains(grant))
                select grant
            );
            _context.ClientGrantTypes.RemoveRange(grantsToDelete);
            var redirectUrisToDelete = (
                from redirectUri in _context.ClientRedirectUris
                where redirectUri.Client == client
                    && (client.RedirectUris == null || !client.RedirectUris.Contains(redirectUri))
                select redirectUri
            );
            _context.ClientRedirectUris.RemoveRange(redirectUrisToDelete);
            var clientRedirectUrisToDelete = (
                from redirectUri in _context.ClientPostLogoutRedirectUris
                where redirectUri.Client == client
                    && (client.PostLogoutRedirectUris == null || !client.PostLogoutRedirectUris.Contains(redirectUri))
                select redirectUri
            );
            _context.ClientPostLogoutRedirectUris.RemoveRange(clientRedirectUrisToDelete);
            var propertiesToDelete = (
                from property in _context.ClientProperties
                where property.Client == client
                    && (client.Properties == null || !client.Properties.Contains(property))
                select property
            );
            _context.ClientProperties.RemoveRange(propertiesToDelete);
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
