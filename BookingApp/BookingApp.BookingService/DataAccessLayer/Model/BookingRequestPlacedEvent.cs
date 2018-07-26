using System;

namespace BookingApp.BookingService.DataAccessLayer.Model
{
    public class BookingRequestPlacedEvent
    {
        public Guid RequestId { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid BookingPropertyId { get; set; }
        public double Price { get; set; }
    }
}
