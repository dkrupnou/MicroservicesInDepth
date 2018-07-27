using System;

namespace BookingApp.PaymentService.ServiceLayer.Model
{
    public class PaidPaymentModel
    {
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }
        public double Amount { get; set; }
    }
}
