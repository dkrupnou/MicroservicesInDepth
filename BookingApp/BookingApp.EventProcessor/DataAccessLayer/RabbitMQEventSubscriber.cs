using BookingApp.EventProcessor.Configuration;
using BookingApp.EventProcessor.DataAccessLayer.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace BookingApp.EventProcessor.DataAccessLayer
{
    public class RabbitMQEventSubscriber : IEventSubscriber
    {
        public event BookingRequestPlacedEventReceivedDelegate BookingRequestPlacedEventReceived;
        public event PaymentPaidEventReceivedDelegate PaymentPaidEventReceived;
        public event PaymentRejectedEventReceivedDelegate PaymentRejectedEventReceived;

        private IConnectionFactory _connectionFactory;
        private QueueSubscriptionOptions _queueOptions;
        private EventingBasicConsumer _consumer;
        private IModel _channel;
        private string _consumerTag;

        public RabbitMQEventSubscriber(
            IConnectionFactory connectionFactory,
            IOptions<QueueSubscriptionOptions> queueOptions,
            EventingBasicConsumer consumer)
        {
            _connectionFactory = connectionFactory;
            _queueOptions = queueOptions.Value;
            _consumer = consumer;
            _channel = consumer.Model;

            Init();
        }

        public void Subscribe()
        {
            _consumerTag = _channel.BasicConsume(_queueOptions.BookingRequestPlacedEventsQueueName, false, _consumer);
        }

        public void Unsubscribe()
        {
            _channel.BasicCancel(_consumerTag);
        }

        private void Init()
        {
            _channel.QueueDeclare(
               queue: _queueOptions.BookingRequestPlacedEventsQueueName,
               durable: false,
               exclusive: false,
               autoDelete: false,
               arguments: null
            );

            _consumer.Received += (ch, ea) => {
                var body = ea.Body;
                var msg = Encoding.UTF8.GetString(body);
                var evt = JsonConvert.DeserializeObject<BookingRequestPlacedEvent>(msg);

                BookingRequestPlacedEventReceived?.Invoke(evt);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
        }
    }
}
