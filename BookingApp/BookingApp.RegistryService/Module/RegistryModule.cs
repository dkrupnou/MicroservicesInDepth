using BookingApp.RegistryService.BusinessLogicLayer;
using BookingApp.RegistryService.Module.Model;
using Nancy;
using Nancy.ModelBinding;

namespace BookingApp.RegistryService.Module
{
    public class RegistryModule : NancyModule
    {
        public RegistryModule(IRegistryService registryService) : base("/registry")
        {
            Get("/{serviceTag}", async parameters =>
            {
                var serviceTag = (string) parameters.serviceTag;
                var urls = await registryService.GetServiceInstancesUrls(serviceTag);
                if (urls.Length == 0)
                    return HttpStatusCode.NotFound;

                return urls;
            });

            Delete("/{serviceTag}/{serviceName}", async parameters =>
            {
                var serviceTag = (string) parameters.serviceTag;
                var serviceName = (string) parameters.serviceName;

                var success = await registryService.UnregisterService(serviceTag, serviceName);
                return success ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            });

            Post("/", async _ =>
            {
                var serviceRegistration = this.Bind<ServiceRegistrationModel>();
                var success = 
                    await registryService.RegisterService(
                        serviceRegistration.ServiceTag,
                        serviceRegistration.ServiceName,
                        serviceRegistration.ServiceUrl
                    );

                return success ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            });
        }
    }
}
