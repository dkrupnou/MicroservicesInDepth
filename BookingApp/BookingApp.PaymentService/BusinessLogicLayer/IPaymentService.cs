using System.Threading.Tasks;
using BookingApp.PaymentService.BusinessLogicLayer.Model;

namespace BookingApp.PaymentService.BusinessLogicLayer
{
    public interface IPaymentService
    {
        Task ProcessPaidPayment(PaidPaymentDetails paidPaymentDetails);
        Task ProcessRejectedPayment(RejectedPaymentDetails rejectedPaymentDetails);
    }
}
