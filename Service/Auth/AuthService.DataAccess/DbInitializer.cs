using AuthService.Common;
using AuthService.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
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
