using System;
using System.Threading.Tasks;
using BookingApp.LocalTrafficRouterService.Client;

namespace BookingApp.LocalTrafficRouterService.BusinessLogicLayer
{
    public class LocalTrafficRouter : ILocalTrafficRouter
    {
        private readonly IRegistryServiceClient _registryClient;

        public LocalTrafficRouter(IRegistryServiceClient registryClient)
        {
            _registryClient = registryClient;
        }

        public async Task<string> Route(string serviceTag)
        {
            try
            {
                var serviceUrls = await _registryClient.GetServiceUrlsByTag(serviceTag);
                return SelectRoutingUrl(serviceUrls);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private string SelectRoutingUrl(string[] urls)
        {
            return urls[new Random().Next(urls.Length)];
        }
    }
}
