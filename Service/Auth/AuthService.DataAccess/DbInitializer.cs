using AuthService.Common;
using AuthService.Common.Models;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.DataAccess
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            }
        }

        public static void AddDefaultIdentityResources(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var count = context.IdentityResources.CountAsync().Result;
                if (count > 0)
                    return;
                var resources = new List<IdentityServer4.EntityFramework.Entities.IdentityResource>
                {
                    ConvertIdentityResource(new IdentityResources.OpenId()),
                    ConvertIdentityResource(new IdentityResources.Email()),
                    ConvertIdentityResource(new IdentityResources.Profile()),
                    ConvertIdentityResource(new IdentityResources.Phone()),
                    ConvertIdentityResource(new IdentityResources.Address())
                };
                context.IdentityResources.AddRange(resources);
                context.SaveChanges();
            }
        }

        private static IdentityServer4.EntityFramework.Entities.IdentityResource ConvertIdentityResource(IdentityResource model)
        {
            var result = new IdentityServer4.EntityFramework.Entities.IdentityResource
            {
                Name = model.Name,
                DisplayName = model.DisplayName,
                Description = model.Description,
                Emphasize = model.Emphasize,
                Enabled = model.Enabled,
                Required = model.Required,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument,
                UserClaims = new List<IdentityServer4.EntityFramework.Entities.IdentityClaim>()
            };

            var userClaims = new List<IdentityServer4.EntityFramework.Entities.IdentityClaim>();
            foreach(var claim in model.UserClaims)
            {
                userClaims.Add(new IdentityServer4.EntityFramework.Entities.IdentityClaim { Type = claim });
            }
            result.UserClaims.AddRange(userClaims);
            return result;
        }

        public static void CreateRoles(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                CreateRolesAsync(scope).Wait();
            }
        }

        private static async Task CreateRolesAsync(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            string[] roleNames = { Const.DefaultRoles.Admin, Const.DefaultRoles.Manager, Const.DefaultRoles.Member };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

            var _user = await userManager.FindByEmailAsync(Const.DefaultUsers.Admin.Email);

            if (_user == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = Const.DefaultUsers.Admin.UserName,
                    Email = Const.DefaultUsers.Admin.Email
                };
                string adminPassword = Const.DefaultUsers.Admin.Password;

                var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Const.DefaultRoles.Admin);
                }
            }
        }

    }
}
