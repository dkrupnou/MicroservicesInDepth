using System;

namespace BookingApp.PaymentService.DataAccessLayer.Model
{
    public class PaymentPaidEvent : PaymentEvent
    {
        public Guid PaymentId { get; set; }
        public double Amount { get; }

        public PaymentPaidEvent(Guid bookingId, Guid paymentId, double amount) : base(bookingId)
        {
            PaymentId = paymentId;
            Amount = amount;
        }
    }
}
