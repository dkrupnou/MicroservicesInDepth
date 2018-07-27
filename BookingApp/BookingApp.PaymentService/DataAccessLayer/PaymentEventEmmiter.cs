using System;
using System.Text;
using System.Threading.Tasks;
using BookingApp.PaymentService.Configuration;
using BookingApp.PaymentService.DataAccessLayer.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace BookingApp.PaymentService.DataAccessLayer
{
    public class PaymentEventEmmiter : IEventEmmiter
    {
        private readonly ConnectionFactory _connectionFactory;
        private static string PaymentProcessedEventsQueueName = "payment-events";

        public PaymentEventEmmiter(IOptions<RabbitMQOptions> options)
        {
            var rabbitOptions = options.Value;
            _connectionFactory = new ConnectionFactory
            {
                UserName = rabbitOptions.Username,
                Password = rabbitOptions.Password,
                VirtualHost = rabbitOptions.VirtualHost,
                HostName = rabbitOptions.HostName,
                Uri = new Uri(rabbitOptions.Uri)
            };
        }

        public async Task EmitEvent(PaymentEvent @event)
        {
            using (IConnection conn = _connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: PaymentProcessedEventsQueueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    string jsonPayload = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(jsonPayload);
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: PaymentProcessedEventsQueueName,
                        basicProperties: null,
                        body: body
                    );
                }
            };
        }
    }
}
