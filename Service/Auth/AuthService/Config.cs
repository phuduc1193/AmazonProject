﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using IdentityServer4.Services;
using IdentityServer4.EntityFramework.Interfaces;
using AuthService.Common;
using AuthService.BusinessLogic;
using AuthService.Common.Interfaces.Services;
using AuthService.DataAccess;
using AuthService.Common.Interfaces.Repositories;
using AuthService.Common.Interfaces.Contexts;
using AuthService.Common.Interfaces.Models;
using AuthService.Common.Models;

namespace AuthService
{
    public static class Config
    {
        public static void AddDbContexts<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {
            var connectionString = configuration.GetConnectionString(Const.ConnectionString);
            var migrationAssembly = configuration.GetConnectionString(Const.MigrationAssembly);
            services.AddDbContext<TContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly)));
        }

        public static IServiceCollection AddAuthenticationServices<TContext, TUser, TRole, TClaimFactory, TProfileService, TConfigurationDbContext, TPersistedGrantDbContext>(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment)
            where TContext : DbContext
            where TUser : class, IApplicationUser
            where TRole : class, IApplicationRole
            where TClaimFactory : class, IUserClaimsPrincipalFactory<TUser>
            where TProfileService : class, IProfileService
            where TConfigurationDbContext : DbContext, IConfigurationDbContext
            where TPersistedGrantDbContext : DbContext, IPersistedGrantDbContext
        {
            var connectionString = configuration.GetConnectionString(Const.ConnectionString);
            var migrationAssembly = configuration.GetConnectionString(Const.MigrationAssembly);

            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
                options.AuthenticationDisplayName = "Windows";
            });

            services.AddIdentity<TUser, TRole>()
                .AddRoleManager<RoleManager<TRole>>()
                .AddEntityFrameworkStores<TContext>()
                .AddClaimsPrincipalFactory<TClaimFactory>()
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
                            sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore<TPersistedGrantDbContext>(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationAssembly));

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
            where TUser : class, IApplicationUser
        {
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<IProfileService, ApplicationProfileService>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();

            services.AddTransient<IApiResourceRepository, ApiResourceRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();

            services.AddTransient<IUserService<TUser>, UserService<TUser>>();
            services.AddTransient<IApiResourceService, ApiResourceService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IClaimService, ClaimService>();

            return services;
        }
    }
}
