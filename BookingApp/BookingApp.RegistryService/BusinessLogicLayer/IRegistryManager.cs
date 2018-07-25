using System;
using System.Threading.Tasks;

namespace BookingApp.RegistryService.BusinessLogicLayer
{
    public interface IRegistryManager
    {
        Task<ServiceInstanceInfo[]> GetServiceInstancesInfo();

        Task<ServiceInstanceInfo[]> GetServiceInstancesInfo(string tag);

        Task<ServiceInstanceInfo> GetServiceInstanceInfo(string tag, string id);

        Task<string[]> GetServiceInstancesUrls(string tag);

        Task<string> RegisterService(string tag, string url);

        Task<bool> UnregisterService(string tag, string serviceId);

        Task<bool> UnregisterServices(string tag);
    }
}