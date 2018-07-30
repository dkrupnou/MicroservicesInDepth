using System;
using BookingApp.EventProcessor.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace BookingApp.EventProcessor.DataAccessLayer
{
    public class RabbitMQConnectionFactory : ConnectionFactory
    {
        protected RabbitMQOptions RabbitMqOptions;

        public RabbitMQConnectionFactory(IOptions<RabbitMQOptions> serviceOptions) : base()
        {
            this.RabbitMqOptions = serviceOptions.Value;

            this.UserName = RabbitMqOptions.Username;
            this.Password = RabbitMqOptions.Password;
            this.VirtualHost = RabbitMqOptions.VirtualHost;
            this.HostName = RabbitMqOptions.HostName;
            this.Uri = new Uri(RabbitMqOptions.Uri);
        }
    }
}
