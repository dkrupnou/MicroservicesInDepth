using System.Threading.Tasks;
using BookingApp.RegistryService.BusinessLogicLayer;
using BookingApp.RegistryService.ServiceLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.RegistryService.ServiceLayer
{
    [Produces("application/json")]
    [Route("registry")]
    public class RegistryController : Controller
    {
        private readonly IRegistryService _registryService;

        public RegistryController(IRegistryService registryService)
        {
            _registryService = registryService;
        }

        [HttpGet]
        [Route("{serviceTag}")]
        public async Task<IActionResult> Get(string serviceTag)
        {
            var urls = await _registryService.GetServiceInstancesUrls(serviceTag);
            if (urls == null || urls.Length == 0)
                return NotFound();

            return Ok(urls);
        }
 
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody]ServiceRegistrationModel serviceRegistration)
        {
            var success =
                await _registryService.RegisterService(
                    serviceRegistration.ServiceTag,
                    serviceRegistration.ServiceName,
                    serviceRegistration.ServiceUrl
                );

            if (!success)
                return NotFound();

            return Ok();
        }
        
        [HttpDelete]
        [Route("{serviceTag}/{serviceName}")]
        public async Task<IActionResult> Delete(string serviceTag, string serviceName)
        {
            var success = await _registryService.UnregisterService(serviceTag, serviceName);
            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
