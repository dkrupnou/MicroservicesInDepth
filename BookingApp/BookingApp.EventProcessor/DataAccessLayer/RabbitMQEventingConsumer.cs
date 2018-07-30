using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BookingApp.EventProcessor.DataAccessLayer
{
    public class RabbitMQEventingConsumer : EventingBasicConsumer
    {
        public RabbitMQEventingConsumer(IConnectionFactory factory) : base(factory.CreateConnection().CreateModel())
        {
        }
    }
}
