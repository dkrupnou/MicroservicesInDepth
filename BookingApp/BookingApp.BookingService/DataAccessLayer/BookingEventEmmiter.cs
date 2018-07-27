using System;
using System.Text;
using System.Threading.Tasks;
using BookingApp.BookingService.Configuration;
using BookingApp.BookingService.DataAccessLayer.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace BookingApp.BookingService.DataAccessLayer
{
    public class BookingEventEmmiter : IEventEmmiter
    {
        private readonly ConnectionFactory _connectionFactory;
        private static string BookingRequestPlacedEventsQueueName = "booking-request-placed";

        public BookingEventEmmiter(IOptions<RabbitMQOptions> options)
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

        public async Task EmitBookingRequestPlacedEvent(BookingRequestPlacedEvent @event)
        {
            using (IConnection conn = _connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: BookingRequestPlacedEventsQueueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    string jsonPayload = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(jsonPayload);
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: BookingRequestPlacedEventsQueueName,
                        basicProperties: null,
                        body: body
                    );
                }
            };
        }
    }
}
