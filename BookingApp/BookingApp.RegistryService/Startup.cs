using BookingApp.RegistryService.BusinessLogicLayer;
using BookingApp.RegistryService.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BookingApp.RegistryService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //By connecting here we are making sure that our service
            //cannot start until redis is ready. This might slow down startup,
            //but given that there is a delay on resolving the ip address
            //and then creating the connection it seems reasonable to move
            //that cost to startup instead of having the first request pay the
            //penalty.
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse("localhost:32783", true);
                configuration.ResolveDns = true;
                return ConnectionMultiplexer.Connect(configuration);
            });

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
