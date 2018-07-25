using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.RegistryService.DataAccessLayer;

namespace BookingApp.RegistryService.BusinessLogicLayer
{
    public class RegistryManager : IRegistryManager
    {
        private readonly IRegistryRepository _repository;

        public RegistryManager(IRegistryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceInstanceInfo[]> GetServiceInstancesInfo()
        {
            var servicesInstances = await _repository.GetAll();
            var servicesByTag = servicesInstances.GroupBy(x => x.Tag);
            var infos = new List<ServiceInstanceInfo>();
            foreach (var serviceByTag in servicesByTag)
            {
                var instancesDetails = serviceByTag.Select(x => new ServiceInstanceInfo() { Id = x.Id, Url = x.Url, Tag = x.Tag });
                infos.AddRange(instancesDetails);
            }
            return infos.ToArray();
        }

        public async Task<ServiceInstanceInfo[]> GetServiceInstancesInfo(string tag)
        {
            var servicesInstances = await _repository.Get(tag);
            return servicesInstances?.Select(x => new ServiceInstanceInfo() { Id = x.Id, Url = x.Url, Tag = x.Tag }).ToArray();
        }

        public async Task<ServiceInstanceInfo> GetServiceInstanceInfo(string tag, string id)
        {
            var guid = Guid.Empty;
            Guid.TryParse(id, out guid);

            var servicesInstances = await _repository.Get(tag);
            var instance = servicesInstances?.FirstOrDefault(x => x.Id == guid);
            if (instance == null)
                return null;

            return new ServiceInstanceInfo()
            {
                Id = instance.Id,
                Url = instance.Url,
                Tag = instance.Tag
            };
        }

        public async Task<string[]> GetServiceInstancesUrls(string tag)
        {
            var servicesInstances = await _repository.Get(tag);
            return servicesInstances?.Select(x => x.Url).ToArray();
        }

        public async Task<string> RegisterService(string tag, string url)
        {
            var entity = new ServiceRegistryEntity()
            {
                Id = Guid.NewGuid(),
                Tag = tag,
                Url = url
            };

            await _repository.Add(entity);
            return entity.Id.ToString();
        }

        public async Task<bool> UnregisterService(string tag, string serviceId)
        {
            var guid = Guid.Empty;
            Guid.TryParse(serviceId, out guid);

            var serviceInstances = await _repository.Get(tag);
            var serviceInstance = serviceInstances?.FirstOrDefault(x => x.Id == guid);
            if (serviceInstance != null)
            {
                await _repository.Remove(serviceInstance);
            }

            return serviceInstance != null;
        }

        public async Task<bool> UnregisterServices(string tag)
        {
            return await _repository.RemoveServicesByTag(tag);
        }
    }
}
