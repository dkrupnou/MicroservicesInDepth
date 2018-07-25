using System.Threading.Tasks;
using BookingApp.LocalTrafficRouterService.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.LocalTrafficRouterService.ServiceLayer
{
    [Produces("application/json")]
    [Route("router")]
    public class LocalTrafficRouterController : Controller
    {
        private readonly ILocalTrafficRouter _router;

        public LocalTrafficRouterController(ILocalTrafficRouter router)
        {
            _router = router;
        }

        [Route("{serviceTag}")]
        public async Task<IActionResult> Get(string serviceTag)
        {
            var routedUrl = await _router.Route(serviceTag);
            if (string.IsNullOrEmpty(routedUrl))
                return NotFound();

            return Ok(routedUrl);
        }
    }
}
