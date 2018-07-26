using System;
using System.Threading.Tasks;
using BookingApp.BookingService.DataAccessLayer;
using BookingApp.BookingService.DataAccessLayer.Model;

namespace BookingApp.BookingService.BusinessLogicLayer
{
    public class BookingService : IBookingService
    {
        private readonly IEventEmmiter _eventEmmiter;

        public BookingService(IEventEmmiter eventEmmiter)
        {
            _eventEmmiter = eventEmmiter;
        }

        public async Task<Guid> BookProperty(Guid properyId, double price)
        {
            var bookingRequestPlacedEvent = new BookingRequestPlacedEvent()
            {
                RequestId = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                BookingPropertyId = properyId,
                Price = price
            };

            await _eventEmmiter.EmitBookingRequestPlacedEvent(bookingRequestPlacedEvent);
            return bookingRequestPlacedEvent.RequestId;
        }
    }
}
