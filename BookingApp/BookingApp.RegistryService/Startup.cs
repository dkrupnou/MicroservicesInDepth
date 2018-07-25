using BookingApp.RegistryService.BusinessLogicLayer;
using BookingApp.RegistryService.DataAccessLayer;
using BookingApp.RegistryService.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BookingApp.RegistryService
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
            services.AddRedisConnectionMultiplexer(Configuration);
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
