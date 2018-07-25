using System.Threading.Tasks;

namespace BookingApp.RegistryService.BusinessLogicLayer
{
    public interface IRegistryManager
    {
        Task<bool> RegisterService(string tag, string name, string url);

        Task<bool> UnregisterService(string tag, string name);

        Task<string[]> GetServiceInstancesUrls(string tag);
    }
}