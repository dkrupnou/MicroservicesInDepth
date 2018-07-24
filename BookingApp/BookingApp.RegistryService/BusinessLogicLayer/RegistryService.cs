using System;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.RegistryService.DataAccessLayer;

namespace BookingApp.RegistryService.BusinessLogicLayer
{
    public class RegistryService : IRegistryService
    {
        private readonly IRegistryRepository _repository;

        public RegistryService(IRegistryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> RegisterService(string tag, string name, string url)
        {
            var entity = new ServiceRegistryEntity()
            {
                Id = Guid.NewGuid(),
                Tag = tag,
                Name = name,
                Url = url
            };

            await _repository.Add(entity);
            return true;
        }

        public async Task<bool> UnregisterService(string tag, string name)
        {
            var serviceInstances = await _repository.Get(tag);
            var serviceInstance = serviceInstances?.FirstOrDefault(x => x.Name.Equals(name));
            return serviceInstance != null;
        }

        public async Task<string[]> GetServiceInstancesUrls(string tag)
        {
            var servicesInstances = await _repository.Get(tag);
            return servicesInstances?.Select(x => x.Url).ToArray();
        }
    }
}
