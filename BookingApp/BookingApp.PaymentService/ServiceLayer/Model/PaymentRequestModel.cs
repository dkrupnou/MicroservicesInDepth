using System;

namespace BookingApp.PaymentService.ServiceLayer.Model
{
    public class PaymentRequestModel
    {
        public Guid BookingId { get; set; }
        public bool Paid { get; set; }
    }
}
