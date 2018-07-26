using System;
using System.Threading.Tasks;

namespace BookingApp.BookingService.BusinessLogicLayer
{
    public interface IBookingService
    {
        Task<Guid> BookProperty(Guid properyId, double price);
    }
}