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
        DbSet<ApiSecret> ApiSecrets { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<IdentityResource> IdentityResources { get; set; }
        DbSet<PersistedGrant> PersistedGrants { get; set; }

        Task<int> SaveChangesAsync();
    }
}