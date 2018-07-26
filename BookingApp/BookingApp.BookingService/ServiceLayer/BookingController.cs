using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.BookingService.ServiceLayer
{
    [Produces("application/json")]
    [Route("booking")]
    public class BookingController : Controller
    {
        public BookingController()
        {  
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            throw new NotImplementedException();
        }
    }
}