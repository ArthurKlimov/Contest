using AutoMapper;
using Contest.BL.Interfaces;
using Contest.BL.Mappings;
using Contest.BL.Services;
using Contest.DA;
using Contest.DA.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Contest.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddDbContext<ContestContext>(options => options.UseSqlServer(_configuration.GetConnectionString("ContestContext")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ContestContext>();

            var mappingConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ContestProfile());
            });

            IMapper mapper = mappingConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IContestService, ContestService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
