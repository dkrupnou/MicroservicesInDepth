using System.Collections.Generic;
using System.Linq;
using BookingApp.RegistryService.Module.Model;
using Nancy;
using Nancy.ModelBinding;

namespace BookingApp.RegistryService.Module
{
    public class RegistryModule : NancyModule
    {
        private static readonly IDictionary<string, List<ServiceRegistrationModel>> Storage = new Dictionary<string, List<ServiceRegistrationModel>>();

        public RegistryModule() : base("/registry")
        {
            Get("/{serviceTag}", parameters =>
            {
                var serviceTag = (string) parameters.serviceTag;
                if (!Storage.ContainsKey(serviceTag))
                    return HttpStatusCode.NotFound;

                var serviceRegistrations = Storage[serviceTag];
                return serviceRegistrations.Select(x => x.ServiceUrl).ToArray();
            });

            Delete("/{serviceTag}/{serviceName}", parameters =>
            {
                var serviceTag = (string) parameters.serviceTag;
                var serviceName = (string) parameters.serviceName;
                if(!Storage.ContainsKey(serviceTag))
                    return HttpStatusCode.NotFound;

                var serviceRegistrations = Storage[serviceTag];
                var serviceInstance = serviceRegistrations.Find(x => x.ServiceName.Equals(serviceName));
                if(serviceInstance == null)
                    return HttpStatusCode.NotFound;

                serviceRegistrations.Remove(serviceInstance);
                if (serviceRegistrations.Count == 0)
                    Storage.Remove(serviceTag);

                return HttpStatusCode.OK;
            });

            Post("/", _ =>
            {
                var serviceRegistration = this.Bind<ServiceRegistrationModel>();
                if (Storage.ContainsKey(serviceRegistration.ServiceTag))
                {
                    var serviceRegistrations = Storage[serviceRegistration.ServiceTag];
                    serviceRegistrations.Add(serviceRegistration);
                }
                else
                {
                    Storage.Add(serviceRegistration.ServiceTag, new List<ServiceRegistrationModel>() { serviceRegistration });
                }

                return HttpStatusCode.OK;
            });
        }
    }
}
