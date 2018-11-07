using System.Threading.Tasks;
using AuthService.Common.Interfaces.Contexts;
using AuthService.Common.Models;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IConfigurationDbContext, ICustomConfigurationDbContext, IPersistedGrantDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<ApiSecret> ApiSecrets { get; set; }
        public DbSet<ApiResourceClaim> ApiClaims { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PersistedGrant>()
                .HasKey(g => g.Key);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
