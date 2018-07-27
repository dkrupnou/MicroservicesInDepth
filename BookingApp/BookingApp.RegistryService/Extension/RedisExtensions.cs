using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BookingApp.RegistryService.Extension
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisConnectionMultiplexer(this IServiceCollection services, IConfiguration config)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var redisConnString = config.GetSection("redis:connectionString").Value;
            services.AddSingleton(typeof(IConnectionMultiplexer), ConnectionMultiplexer.Connect(redisConnString));
            return services;
        }
    }
}
