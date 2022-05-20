using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EventBus.Messages.Extensions
{
    public static class RabbitMqExtension
    {
        public static IServiceCollection AddMasstransitWithRabbitMQ(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<IApplicationBuilder>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            services.AddMassTransit(x => {
                x.AddConsumers(Assembly.GetEntryAssembly());
                x.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(new Uri(configuration["EventBusSettings:HostAddress"]), host =>
                    {
                        host.Username(configuration["EventBusSettings:Username"]);
                        host.Password(configuration["EventBusSettings:Password"]);
                    });
                    cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter(configuration["ServiceName"], false));
                });
            });

            services.AddGenericRequestClient();

            services.AddMassTransitHostedService(true);

            return services;
        }
    }
}
