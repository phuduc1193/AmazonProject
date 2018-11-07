using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using AuthService.Common.Models;
using AuthService.DataAccess;
using AuthService.BusinessLogic;

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
            services.AddDbContexts<ApplicationDbContext>(Configuration);
            services.AddAuthenticationServices<ApplicationDbContext, ApplicationUser, ApplicationRole, AppClaimsPrincipalFactory<ApplicationUser, ApplicationRole>, ApplicationProfileService, ApplicationDbContext, ApplicationDbContext>(Configuration, Environment);
            services.AddDependencies<ApplicationUser>();
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
