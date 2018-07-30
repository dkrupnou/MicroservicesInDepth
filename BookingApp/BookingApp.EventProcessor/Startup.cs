using BookingApp.EventProcessor.Configuration;
using BookingApp.EventProcessor.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BookingApp.EventProcessor
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
            services.Configure<QueueSubscriptionOptions>(_configuration.GetSection("subscriptions"));

            services.AddTransient<IConnectionFactory, RabbitMQConnectionFactory>();
            services.AddTransient(typeof(EventingBasicConsumer), typeof(RabbitMQEventingConsumer));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
