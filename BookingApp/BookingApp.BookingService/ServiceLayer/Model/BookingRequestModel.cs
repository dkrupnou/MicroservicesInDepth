using System;

namespace BookingApp.BookingService.ServiceLayer.Model
{
    public class BookingRequestModel
    {
        public Guid PropertyId { get; set; }
        public Guid CustomerId { get; set; }
        public BookingDatesModel Dates { get; set; }
    }
}
