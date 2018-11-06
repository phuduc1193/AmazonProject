using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using IdentityServer4.Services;
using IdentityServer4.EntityFramework.Interfaces;
using AuthService.Common;
using AuthService.BusinessLogic;
using AuthService.Common.Interfaces.Services;
using AuthService.DataAccess;
using AuthService.Common.Interfaces.Repositories;
using AuthService.Common.Interfaces.Contexts;
using AuthService.BusinessLogic.DbContexts;

namespace AuthService
{
    public static class Config
    {
        public static void AddDbContexts<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {
            var connectionString = configuration.GetConnectionString(Const.ConnectionString);
            services.AddDbContext<TContext>(options => options.UseSqlServer(connectionString));
        }

        public static IServiceCollection AddAuthenticationServices<TContext, TUser, TRole, TProfileService, TConfigurationDbContext>(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment)
            where TContext : DbContext
            where TUser : class
            where TRole : class
            where TProfileService : class, IProfileService
            where TConfigurationDbContext : DbContext, IConfigurationDbContext
        {
            var connectionString = configuration.GetConnectionString(Const.ConnectionString);
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
                options.AuthenticationDisplayName = "Windows";
            });

            services.AddIdentity<TUser, TRole>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders();

            var identityServer = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddAspNetIdentity<TUser>()
                .AddProfileService<TProfileService>()
                .AddConfigurationStore<TConfigurationDbContext>(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });

            if (environment.IsDevelopment())
            {
                identityServer.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            return services;
        }

        public static IServiceCollection AddDependencies<TUser>(this IServiceCollection services)
            where TUser : class
        {
            services.AddTransient<ICustomConfigurationDbContext, CustomConfigurationDbContext>();

            services.AddTransient<IApiResourceRepository, ApiResourceRepository>();

            services.AddTransient<IUserService<TUser>, UserService<TUser>>();
            services.AddTransient<IApiResourceService, ApiResourceService>();

            return services;
        }
    }
}
