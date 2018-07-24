using BookingApp.RegistryService.BusinessLogicLayer;
using BookingApp.RegistryService.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.RegistryService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRegistryRepository, InMemoryRegistryRepository>();
            services.AddScoped<IRegistryService, BusinessLogicLayer.RegistryService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
