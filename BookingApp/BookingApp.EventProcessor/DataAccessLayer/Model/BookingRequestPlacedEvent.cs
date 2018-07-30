using System;

namespace BookingApp.EventProcessor.DataAccessLayer.Model
{
    public class BookingRequestPlacedEvent
    {
        public DateTime Timestamp { get; set; }
        public Guid RequestId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
