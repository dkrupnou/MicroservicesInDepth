using BookingApp.PaymentService.BusinessLogicLayer;
using BookingApp.PaymentService.Configuration;
using BookingApp.PaymentService.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.PaymentService
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

            services.AddSingleton<IEventEmmiter, PaymentEventEmmiter>();
            services.AddTransient<IPaymentService, BusinessLogicLayer.PaymentService>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
