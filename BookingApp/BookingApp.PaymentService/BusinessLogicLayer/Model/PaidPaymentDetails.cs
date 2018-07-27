using System;

namespace BookingApp.PaymentService.BusinessLogicLayer.Model
{
    public class PaidPaymentDetails
    {
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Amount { get; set; }
    }
}
