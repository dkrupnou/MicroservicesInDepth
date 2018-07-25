using System;
using System.Threading.Tasks;
using BookingApp.LocalTrafficRouterService.Client;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.LocalTrafficRouterService.ServiceLayer
{
    [Produces("application/json")]
    [Route("router")]
    public class LocalTrafficRouterController : Controller
    {
        private readonly IRegistryServiceClient _registryServiceClient;

        public LocalTrafficRouterController(IRegistryServiceClient registryServiceClient)
        {
            _registryServiceClient = registryServiceClient;
        }

        [Route("{serviceTag}")]
        public async Task<IActionResult> Get(string serviceTag)
        {
            try
            {
                var serviceUrls = await _registryServiceClient.GetServiceUrlsByTag(serviceTag);
                return Ok(serviceUrls[new Random().Next(serviceUrls.Length)]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }
    }
}
