using System.Threading.Tasks;
using BookingApp.PaymentService.DataAccessLayer.Model;

namespace BookingApp.PaymentService.DataAccessLayer
{
    public interface IEventEmmiter
    {
        Task EmitEvent(PaymentEvent @event);
    }
}