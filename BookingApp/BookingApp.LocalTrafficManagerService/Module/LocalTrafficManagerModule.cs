using BookingApp.LocalTrafficManagerService.Client;
using Nancy;
using System;

namespace BookingApp.LocalTrafficManagerService.Module
{
    public class LocalTrafficManagerModule : NancyModule
    {
        public LocalTrafficManagerModule(IRegistryServiceClient registryServiceClient) : base("/traffic")
        {
            Get("/{serviceTag}", async parameters =>
            {
                try
                {
                    var serviceTag = (string) parameters.serviceTag;
                    var serviceUrls = await registryServiceClient.GetServiceUrlsByTag(serviceTag);
                    return serviceUrls[new Random().Next(serviceUrls.Length)];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return HttpStatusCode.NotFound;
                }
            });
        }
    }
}
