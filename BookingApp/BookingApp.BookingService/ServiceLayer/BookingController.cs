using System.Threading.Tasks;
using BookingApp.BookingService.BusinessLogicLayer;
using BookingApp.BookingService.ServiceLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.BookingService.ServiceLayer
{
    [Produces("application/json")]
    [Route("booking")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BookingRequestModel request)
        {
            var bookingRequest = new BookingRequest()
            {
                PropertyId = request.PropertyId,
                CustomerId = request.CustomerId,
                From = request.Dates.From,
                To = request.Dates.To
            };
            var requestId = await _bookingService.BookProperty(bookingRequest);
            return Ok(requestId);
        }
    }
}