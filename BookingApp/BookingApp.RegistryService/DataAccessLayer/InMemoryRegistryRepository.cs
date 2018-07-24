using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.RegistryService.DataAccessLayer
{
    public class InMemoryRegistryRepository : IRegistryRepository
    {
        private readonly IDictionary<string, List<ServiceRegistryEntity>> _storage = new Dictionary<string, List<ServiceRegistryEntity>>();

        public async Task<ServiceRegistryEntity> Add(ServiceRegistryEntity entity)
        {
            if (_storage.ContainsKey(entity.Tag))
            {
                var serviceInstances = _storage[entity.Tag];
                serviceInstances.Add(entity);
            }
            else
            {
                _storage.Add(entity.Tag, new List<ServiceRegistryEntity>(){entity});
            }

            return await Task.FromResult(entity);
        }

        public async Task<ServiceRegistryEntity> Remove(ServiceRegistryEntity entity)
        {
            if (!_storage.ContainsKey(entity.Tag))
                return await Task.FromResult<ServiceRegistryEntity>(null);

            var serviceInstances = _storage[entity.Tag];
            serviceInstances.Remove(entity);

            if (serviceInstances.Count == 0)
                _storage.Remove(entity.Tag);

            return await Task.FromResult(entity);
        }

        public async Task<ServiceRegistryEntity[]> Get(string serviceTag)
        {
            if (!_storage.ContainsKey(serviceTag))
                return await Task.FromResult<ServiceRegistryEntity[]>(null);

            return await Task.FromResult(_storage[serviceTag].ToArray());
        }
    }
}
