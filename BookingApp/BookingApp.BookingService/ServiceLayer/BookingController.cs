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
        public async Task<IActionResult> Post([FromBody]BookingRequestModel bookingRequest)
        {
            var requestId = await _bookingService.BookProperty(bookingRequest.ProperyId, bookingRequest.Price);
            return Ok(requestId);
        }
    }
}