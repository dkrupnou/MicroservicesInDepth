using BookingApp.LocalTrafficRouterService.BusinessLogicLayer;
using BookingApp.LocalTrafficRouterService.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.LocalTrafficRouterService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var registryServiceUrl = _configuration.GetSection("registry:url").Value;
            services.AddTransient<IRegistryServiceClient>(x => new RegistryServiceClient(registryServiceUrl));
            services.AddTransient<ILocalTrafficRouter, LocalTrafficRouter>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
