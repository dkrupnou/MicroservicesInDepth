using System;
using BookingApp.RegistryService.BusinessLogicLayer;
using BookingApp.RegistryService.DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace BookingApp.RegistryService
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

            container.Register(_serviceProvider.GetService<IRegistryRepository>());
            container.Register(_serviceProvider.GetService<IRegistryService>()).AsMultiInstance();
        }
    }
}
