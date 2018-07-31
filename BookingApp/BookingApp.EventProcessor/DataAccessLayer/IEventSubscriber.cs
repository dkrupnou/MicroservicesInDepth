using BookingApp.EventProcessor.DataAccessLayer.Model;

namespace BookingApp.EventProcessor.DataAccessLayer
{
    public delegate void BookingRequestPlacedEventReceivedDelegate(BookingRequestPlacedEvent @event);
    public delegate void PaymentPaidEventReceivedDelegate(PaymentPaidEvent @event);
    public delegate void PaymentRejectedEventReceivedDelegate(PaymentRejectedEvent @event);

    public interface IEventSubscriber
    {
        event BookingRequestPlacedEventReceivedDelegate BookingRequestPlacedEventReceived;
        event PaymentPaidEventReceivedDelegate PaymentPaidEventReceived;
        event PaymentRejectedEventReceivedDelegate PaymentRejectedEventReceived;

        void Subscribe();
        void Unsubscribe();
    }
}
