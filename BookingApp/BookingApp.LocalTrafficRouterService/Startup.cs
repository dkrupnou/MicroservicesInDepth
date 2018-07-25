using BookingApp.LocalTrafficRouterService.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.LocalTrafficRouterService
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var registryServiceUrl = Configuration["RegistryServiceUrl"];
            services.AddTransient<IRegistryServiceClient>(x => new RegistryServiceClient(registryServiceUrl));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
