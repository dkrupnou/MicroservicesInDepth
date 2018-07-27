using System.Threading.Tasks;
using BookingApp.BookingService.DataAccessLayer.Model;

namespace BookingApp.BookingService.DataAccessLayer
{
    public interface IEventEmmiter
    {
        Task EmitBookingRequestPlacedEvent(BookingRequestPlacedEvent @event);
    }
}