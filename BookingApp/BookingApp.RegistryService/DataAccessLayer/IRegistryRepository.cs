using System.Threading.Tasks;

namespace BookingApp.RegistryService.DataAccessLayer
{
    public interface IRegistryRepository
    {
        Task<ServiceRegistryEntity> Add(ServiceRegistryEntity entity);
        Task<ServiceRegistryEntity> Remove(ServiceRegistryEntity entity);
        Task<ServiceRegistryEntity[]> Get(string serviceTag);
        Task<ServiceRegistryEntity[]> GetAll();
        Task<bool> RemoveServicesByTag(string serviceTag);
    }
}