using System;

namespace BookingApp.PaymentService.DataAccessLayer.Model
{
    public class PaymentProcessedEvent
    {
        public DateTime Timestamp { get; set; }
        public Guid RequestId { get; set; }
        public Guid BookingId { get; set; }
        public bool Paid { get; set; }
    }
}
