using System;
using BookingApp.LocalTrafficManagerService.Client;
using Microsoft.Extensions.DependencyInjection;
using Nancy;
using Nancy.TinyIoc;

namespace BookingApp.LocalTrafficManagerService
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly IServiceProvider _serviceProvider;

        public Bootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register(_serviceProvider.GetService<IRegistryServiceClient>());
        }
    }
}
