using BookingApp.RegistryService.BusinessLogicLayer;
using BookingApp.RegistryService.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;

namespace BookingApp.RegistryService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRegistryRepository, InMemoryRegistryRepository>();
            services.AddSingleton<IRegistryService, BusinessLogicLayer.RegistryService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseOwin(x => x.UseNancy(options =>
                {
                    options.Bootstrapper = new Bootstrapper(app.ApplicationServices);
                })
            );
        }
    }
}
