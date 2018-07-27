using System.Threading.Tasks;
using BookingApp.PaymentService.BusinessLogicLayer;
using BookingApp.PaymentService.BusinessLogicLayer.Model;
using BookingApp.PaymentService.ServiceLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.PaymentService.ServiceLayer
{
    [Produces("application/json")]
    [Route("payment")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("paid")]
        public async Task<IActionResult> PostPaid([FromBody]PaidPaymentModel model)
        {
            var details = new PaidPaymentDetails()
            {
                PaymentId = model.PaymentId,
                BookingId = model.BookingId,
                Amount = model.Amount
            };

            await _paymentService.ProcessPaidPayment(details);
            return Ok();
        }

        [HttpPost]
        [Route("rejected")]
        public async Task<IActionResult> PostRejected([FromBody]RejectedPaymentModel model)
        {
            var details = new RejectedPaymentDetails()
            {
                BookingId = model.BookingId,
                Reason = model.Reason
            };

            await _paymentService.ProcessRejectedPayment(details);
            return Ok();
        }
    }
}