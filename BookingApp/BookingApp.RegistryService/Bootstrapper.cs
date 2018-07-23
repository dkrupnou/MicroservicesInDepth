using System;
using Nancy;
using Nancy.Bootstrapper;

namespace BookingApp.RegistryService
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override
            Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration =>
            NancyInternalConfiguration
                .WithOverrides(builder => builder.StatusCodeHandlers.Clear());
    }
}
