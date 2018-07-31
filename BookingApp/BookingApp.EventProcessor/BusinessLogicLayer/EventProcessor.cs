using BookingApp.EventProcessor.DataAccessLayer;
using BookingApp.EventProcessor.DataAccessLayer.Model;
using System;

namespace BookingApp.EventProcessor.BusinessLogicLayer
{
    public class EventProcessor : IEventProcessor
    {
        private IEventSubscriber _subscriber;

        public EventProcessor(IEventSubscriber subscriber)
        {
            _subscriber = subscriber;
            _subscriber.BookingRequestPlacedEventReceived += OnBookingRequestPlacedEventReceived;
        }

        public void Start()
        {
            _subscriber.Subscribe();
        }

        public void Stop()
        {
            _subscriber.Unsubscribe();
        }

        private void OnBookingRequestPlacedEventReceived(BookingRequestPlacedEvent @event)
        {
            Console.WriteLine("OnBookingRequestPlacedEventReceived");
        }
    }
}
