using BookingApp.RegistryService.BusinessLogicLayer;
using BookingApp.RegistryService.DataAccessLayer;
using BookingApp.RegistryService.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.RegistryService
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
            services.AddRedisConnectionMultiplexer(_configuration);
            services.AddTransient<IRegistryRepository, RedisRegistryRepository>();
            services.AddTransient<IRegistryManager, RegistryManager>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
