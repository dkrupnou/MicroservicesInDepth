using System;
using System.Threading.Tasks;
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

        public async Task<Guid> ProcessPayment(Guid bookingId, bool paid)
        {
            var paymentProcessedEvent = new PaymentProcessedEvent()
            {
                Timestamp = DateTime.UtcNow,
                RequestId = Guid.NewGuid(),
                BookingId = bookingId,
                Paid = paid
            };

            await _eventEmmiter.EmitPaymentProcessedEvent(paymentProcessedEvent);
            return paymentProcessedEvent.RequestId;
        }
    }
}
