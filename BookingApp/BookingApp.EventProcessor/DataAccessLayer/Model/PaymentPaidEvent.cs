using System;

namespace BookingApp.EventProcessor.DataAccessLayer.Model
{
    public class PaymentPaidEvent
    {
        public Guid BookingId { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid PaymentId { get; set; }
        public double Amount { get; set; }
    }
}
