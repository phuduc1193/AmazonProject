using System.Threading.Tasks;
using AuthService.Common.Interfaces.Contexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.BusinessLogic.DbContexts
{
    public class CustomConfigurationDbContext : DbContext, IConfigurationDbContext, ICustomConfigurationDbContext
    {
        public CustomConfigurationDbContext(DbContextOptions<CustomConfigurationDbContext> options)
            : base(options) { }

        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<ApiSecret> ApiSecrets { get; set; }
        public DbSet<ApiResourceClaim> ApiClaims { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            var task = Task.Run(() => SaveChanges());
            return await task;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
