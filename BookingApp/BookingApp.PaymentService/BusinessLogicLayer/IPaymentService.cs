using System;
using System.Threading.Tasks;

namespace BookingApp.PaymentService.BusinessLogicLayer
{
    public interface IPaymentService
    {
        Task<Guid> ProcessPayment(Guid bookingId, bool paid);
    }
}
