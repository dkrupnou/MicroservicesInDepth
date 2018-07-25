using System;

namespace BookingApp.RegistryService.BusinessLogicLayer
{
    public class ServiceInstancesDetails
    {
        public string Tag { get; set; }

        public ServiceInstanceInfo[] Instances { get; set; }
    }

    public class ServiceInstanceInfo
    {
        public Guid Id { get; set; }

        public string Tag { get; set; }

        public string Url { get; set; }
    }
}
