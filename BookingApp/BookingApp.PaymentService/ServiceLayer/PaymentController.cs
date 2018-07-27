using System.Threading.Tasks;
using BookingApp.PaymentService.BusinessLogicLayer;
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
        public async Task<IActionResult> Post([FromBody]PaymentRequestModel request)
        {
            var paymentId = await _paymentService.ProcessPayment(request.BookingId, request.Paid);
            return Ok(paymentId);
        }
    }
}