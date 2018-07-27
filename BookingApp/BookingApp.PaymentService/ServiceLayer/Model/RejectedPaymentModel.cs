using System;

namespace BookingApp.PaymentService.ServiceLayer.Model
{
    public class RejectedPaymentModel
    {
        public Guid BookingId { get; set; }
        public string Reason { get; set; }
    }
}
