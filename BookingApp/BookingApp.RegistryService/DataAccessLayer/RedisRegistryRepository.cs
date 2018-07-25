using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BookingApp.RegistryService.DataAccessLayer
{
    public class RedisRegistryRepository : IRegistryRepository
    {
        private readonly IDatabaseAsync _db;

        public RedisRegistryRepository(ConnectionMultiplexer multiplexer)
        {
            _db = multiplexer.GetDatabase();
        }

        public async Task<ServiceRegistryEntity> Add(ServiceRegistryEntity entity)
        {
            var serviceTag = entity.Tag;
            if (await _db.KeyExistsAsync(serviceTag))
            {
                var serviceInstancesJson = await _db.StringGetAsync(serviceTag);
                var serviceInstances = JsonConvert.DeserializeObject<IList<ServiceRegistryEntity>>(serviceInstancesJson);
                serviceInstances.Add(entity);
                await _db.StringSetAsync(serviceTag, JsonConvert.SerializeObject(serviceInstances));
            }
            else
            {
                await _db.StringSetAsync(serviceTag, JsonConvert.SerializeObject(new List<ServiceRegistryEntity>() { entity }));
            }

            return entity;
        }

        public async Task<ServiceRegistryEntity> Remove(ServiceRegistryEntity entity)
        {
            var serviceTag = entity.Tag;
            var serviceTagExists = await _db.KeyExistsAsync(serviceTag);
            if (!serviceTagExists)
                return null;

            var serviceInstancesJson = await _db.StringGetAsync(serviceTag);
            var serviceInstances = JsonConvert.DeserializeObject<IList<ServiceRegistryEntity>>(serviceInstancesJson);
            serviceInstances.Remove(serviceInstances.First(x => x.Id == entity.Id));

            if (serviceInstances.Count != 0)
            {
                await _db.StringSetAsync(serviceTag, JsonConvert.SerializeObject(serviceInstances));
            }
            else
            {
                await _db.KeyDeleteAsync(serviceTag);
            }
                
            return entity;
        }

        public async Task<ServiceRegistryEntity[]> Get(string serviceTag)
        {
            var serviceTagExists = await _db.KeyExistsAsync(serviceTag);
            if (!serviceTagExists)
                return null;

            var serviceInstancesJson = await _db.StringGetAsync(serviceTag);
            var serviceInstances = JsonConvert.DeserializeObject<IList<ServiceRegistryEntity>>(serviceInstancesJson);
            return serviceInstances.ToArray();
        }
    }
}
