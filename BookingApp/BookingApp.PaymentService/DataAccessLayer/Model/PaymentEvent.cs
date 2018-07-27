using System;

namespace BookingApp.PaymentService.DataAccessLayer.Model
{
    public class PaymentEvent
    {
        public Guid BookingId { get; }
        public DateTime Timestamp { get; }

        public PaymentEvent(Guid bookingId)
        {
            BookingId = bookingId;
            Timestamp = DateTime.UtcNow;
        }
    }
}
