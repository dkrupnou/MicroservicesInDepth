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
        private readonly string _servicesTagsKey = "tags";

        public RedisRegistryRepository(ConnectionMultiplexer multiplexer)
        {
            _db = multiplexer.GetDatabase();
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

        public async Task<ServiceRegistryEntity[]> GetAll()
        {
            var servicesTagsJson = await _db.StringGetAsync(_servicesTagsKey);
            if(servicesTagsJson.IsNull)
                return new ServiceRegistryEntity[]{};

            var serviceTags = JsonConvert.DeserializeObject<IList<string>>(servicesTagsJson);
            var allEntities = new List<ServiceRegistryEntity>();
            foreach (var serviceTag in serviceTags)
            {
                var entitiesByTag = await Get(serviceTag);
                allEntities.AddRange(entitiesByTag);
            }

            return allEntities.ToArray();
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
                var servicesTagsJson = await _db.StringGetAsync(_servicesTagsKey);
                if (servicesTagsJson.IsNull)
                {
                    await _db.StringSetAsync(_servicesTagsKey, JsonConvert.SerializeObject(new List<string>() { serviceTag }));
                }
                else
                {
                    var servicesTags = JsonConvert.DeserializeObject<IList<string>>(servicesTagsJson);
                    servicesTags.Add(serviceTag);
                    await _db.StringSetAsync(_servicesTagsKey, JsonConvert.SerializeObject(servicesTags));
                }

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
                var servicesTagsJson = await _db.StringGetAsync(_servicesTagsKey);
                var servicesTags = JsonConvert.DeserializeObject<IList<string>>(servicesTagsJson);
                servicesTags.Remove(serviceTag);
                await _db.StringSetAsync(_servicesTagsKey, JsonConvert.SerializeObject(servicesTags));
                await _db.KeyDeleteAsync(serviceTag);
            }
                
            return entity;
        }

        public async Task<bool> RemoveServicesByTag(string serviceTag)
        {
            var serviceTagExists = await _db.KeyExistsAsync(serviceTag);
            if (!serviceTagExists)
                return false;

            var servicesTagsJson = await _db.StringGetAsync(_servicesTagsKey);
            var servicesTags = JsonConvert.DeserializeObject<IList<string>>(servicesTagsJson);
            servicesTags.Remove(serviceTag);
            await _db.StringSetAsync(_servicesTagsKey, JsonConvert.SerializeObject(servicesTags));
            await _db.KeyDeleteAsync(serviceTag);
            return true;
        }
    }
}
