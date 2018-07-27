using System;

namespace BookingApp.PaymentService.BusinessLogicLayer.Model
{
    public class RejectedPaymentDetails
    {
        public Guid BookingId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Reason { get; set; }
    }
}
