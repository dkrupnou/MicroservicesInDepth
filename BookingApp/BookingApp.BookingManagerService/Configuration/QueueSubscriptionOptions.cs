namespace BookingApp.BookingManagerService.Configuration
{
    public class QueueSubscriptionOptions
    {
        public string BookingRequestPlacedEventsQueueName { get; set; }
        public string PaymentProcessedEventsQueueName { get; set; }
    }
}
