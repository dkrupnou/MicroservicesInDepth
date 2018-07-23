using BookingApp.LocalTrafficManagerService.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;

namespace BookingApp.LocalTrafficManagerService
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
            services.AddSingleton<IRegistryServiceClient>(x => new RegistryServiceClient(registryServiceUrl));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy(options =>
                {
                    options.Bootstrapper = new Bootstrapper(app.ApplicationServices);
                })
            );
        }
    }
}
