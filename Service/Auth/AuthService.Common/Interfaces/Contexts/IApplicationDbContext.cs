using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Common.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        DbSet<ApiResourceClaim> ApiClaims { get; set; }
        DbSet<ApiResource> ApiResources { get; set; }
        DbSet<ApiScope> ApiScopes { get; set; }
        DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }
        DbSet<ApiSecret> ApiSecrets { get; set; }
        DbSet<ClientClaim> ClientClaims { get; set; }
        DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        DbSet<ClientGrantType> ClientGrantTypes { get; set; }
        DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        DbSet<ClientProperty> ClientProperties { get; set; }
        DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<ClientScope> ClientScopes { get; set; }
        DbSet<ClientSecret> ClientSecrets { get; set; }
        DbSet<IdentityClaim> IdentityClaims { get; set; }
        DbSet<IdentityResource> IdentityResources { get; set; }
        DbSet<PersistedGrant> PersistedGrants { get; set; }

        Task<int> SaveChangesAsync();
    }
}