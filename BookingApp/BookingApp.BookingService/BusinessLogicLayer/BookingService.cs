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

        public async Task<Guid> BookProperty(BookingRequest request)
        {
            var bookingRequestPlacedEvent = new BookingRequestPlacedEvent()
            {
                Timestamp = DateTime.UtcNow,
                RequestId = Guid.NewGuid(),
                PropertyId = request.PropertyId,
                CustomerId = request.CustomerId,
                From = request.From,
                To = request.To
            };

            await _eventEmmiter.EmitBookingRequestPlacedEvent(bookingRequestPlacedEvent);
            return bookingRequestPlacedEvent.RequestId;
        }
    }
}
