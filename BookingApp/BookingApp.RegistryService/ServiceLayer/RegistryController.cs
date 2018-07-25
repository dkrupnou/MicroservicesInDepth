using System;
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
        private readonly IRegistryManager _registryManager;

        public RegistryController(IRegistryManager registryManager)
        {
            _registryManager = registryManager;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var details = await _registryManager.GetServiceInstancesInfo();
            return Ok(details);
        }

        [HttpGet]
        [Route("{serviceTag}")]
        public async Task<IActionResult> Get(string serviceTag)
        {
            var details = await _registryManager.GetServiceInstancesInfo(serviceTag);
            if (details == null)
                return NotFound();

            return Ok(details);
        }

        [HttpGet]
        [Route("{serviceTag}/{serviceId}")]
        public async Task<IActionResult> Get(string serviceTag, string serviceId)
        {
            var details = await _registryManager.GetServiceInstanceInfo(serviceTag, serviceId);
            if (details == null)
                return NotFound();

            return Ok(details);
        }

        [HttpGet]
        [Route("{serviceTag}/urls")]
        public async Task<IActionResult> GetUrlsByTag(string serviceTag)
        {
            var urls = await _registryManager.GetServiceInstancesUrls(serviceTag);
            if (urls == null || urls.Length == 0)
                return NotFound();

            return Ok(urls);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody]ServiceRegistrationModel serviceRegistration)
        {
            var serviceId = await _registryManager.RegisterService(serviceRegistration.ServiceTag, serviceRegistration.ServiceUrl);
            return Ok(serviceId);
        }
        
        [HttpDelete]
        [Route("{serviceTag}/{serviceId}")]
        public async Task<IActionResult> Delete(string serviceTag, string serviceId)
        {
            var wasRemoved = await _registryManager.UnregisterService(serviceTag, serviceId);
            if (!wasRemoved)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Route("{serviceTag}")]
        public async Task<IActionResult> Delete(string serviceTag)
        {
            var wasRemoved = await _registryManager.UnregisterServices(serviceTag);
            if (!wasRemoved)
                return NotFound();

            return Ok();
        }
    }
}
