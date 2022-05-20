using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Extensions
{
    public static class RabbitMqExtension
    {
        public static IServiceCollection AddMasstransitWithRabbitMQ(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<IApplicationBuilder>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            services.AddMassTransit(x => {
                x.AddConsumers(Assembly.GetEntryAssembly());
                x.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(configuration["EventBusSettings:HostAddress"], host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
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
