using System;

namespace BookingApp.RegistryService.DataAccessLayer
{
    public class ServiceRegistryEntity
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
