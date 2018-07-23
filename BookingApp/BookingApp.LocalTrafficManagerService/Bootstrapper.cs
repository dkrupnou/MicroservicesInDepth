using System;
using BookingApp.LocalTrafficManagerService.Client;
using Microsoft.Extensions.DependencyInjection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace BookingApp.LocalTrafficManagerService
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override
            Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration =>
            NancyInternalConfiguration
                .WithOverrides(builder => builder.StatusCodeHandlers.Clear());

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
