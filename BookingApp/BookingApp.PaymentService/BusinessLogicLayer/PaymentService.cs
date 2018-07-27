using System.Threading.Tasks;
using BookingApp.PaymentService.BusinessLogicLayer.Model;
using BookingApp.PaymentService.DataAccessLayer;
using BookingApp.PaymentService.DataAccessLayer.Model;

namespace BookingApp.PaymentService.BusinessLogicLayer
{
    public class PaymentService : IPaymentService
    {
        private readonly IEventEmmiter _eventEmmiter;

        public PaymentService(IEventEmmiter eventEmmiter)
        {
            _eventEmmiter = eventEmmiter;
        }

        public async Task ProcessPaidPayment(PaidPaymentDetails paidPaymentDetails)
        {
            var paymentPaidEvent = new PaymentPaidEvent(paidPaymentDetails.BookingId, paidPaymentDetails.PaymentId, paidPaymentDetails.Amount);
            await _eventEmmiter.EmitEvent(paymentPaidEvent);
        }

        public async Task ProcessRejectedPayment(RejectedPaymentDetails rejectedPaymentDetails)
        {
            var paymentRejectedEvent = new PaymentRejectedEvent(rejectedPaymentDetails.BookingId, rejectedPaymentDetails.Reason);
            await _eventEmmiter.EmitEvent(paymentRejectedEvent);
        }
    }
}
