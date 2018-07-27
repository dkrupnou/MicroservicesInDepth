using System;

namespace BookingApp.BookingService.BusinessLogicLayer
{
    public class BookingRequest
    {
        public Guid PropertyId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
