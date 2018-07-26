using System;

namespace BookingApp.BookingService.ServiceLayer.Model
{
    public class BookingRequestModel
    {
        public Guid ProperyId { get; set; }
        public double Price { get; set; }
    }
}
