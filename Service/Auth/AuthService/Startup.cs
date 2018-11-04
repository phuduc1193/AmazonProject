using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using IdentityServer4;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using AuthService.Data;
using AuthService.Helpers;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Linq;

namespace AuthService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            Configuration = config;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
                options.AuthenticationDisplayName = "Windows";
            });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<UserDbContext>(options => options.UseSqlite(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            var identityServer = Config.IdentityServer(services, Configuration, connectionString);
            if (Environment.IsDevelopment())
            {
                identityServer.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddAuthentication()
                .AddGoogle(Const.ExternalProvider.Google, Configuration["Authentication:Google:DisplayName"], options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                    //})
                    //.AddFacebook("Facebook", Configuration["Authentication:Facebook:DisplayName"], options =>
                    //{
                    //    options.AppId = Configuration["Authentication:Facebook:AppId"];
                    //    options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                });

            services.AddTransient<ILoginService<ApplicationUser>, LoginService>();
            services.AddTransient<IRegistrationService<ApplicationUser>, RegistrationService>();
            services.AddTransient<IApiResourceService, ApiResourceService>();

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
