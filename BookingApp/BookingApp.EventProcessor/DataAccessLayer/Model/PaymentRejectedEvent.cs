using System;

namespace BookingApp.EventProcessor.DataAccessLayer.Model
{
    public class PaymentRejectedEvent
    {
        public Guid BookingId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Reason { get; set; }
    }
}
