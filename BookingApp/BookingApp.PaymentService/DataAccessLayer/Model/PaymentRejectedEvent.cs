using System;

namespace BookingApp.PaymentService.DataAccessLayer.Model
{
    public class PaymentRejectedEvent : PaymentEvent
    {
        public string Reason { get; }

        public PaymentRejectedEvent(Guid bookingId, string reason) : base(bookingId)
        {
            Reason = reason;
        }
    }
}
