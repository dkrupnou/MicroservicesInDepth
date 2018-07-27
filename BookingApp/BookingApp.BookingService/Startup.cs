using BookingApp.BookingService.BusinessLogicLayer;
using BookingApp.BookingService.Configuration;
using BookingApp.BookingService.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.BookingService
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
            services.AddOptions();
            services.Configure<RabbitMQOptions>(_configuration.GetSection("rabbitmq"));

            services.AddSingleton<IEventEmmiter, BookingEventEmmiter>();
            services.AddTransient<IBookingService, BusinessLogicLayer.BookingService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
