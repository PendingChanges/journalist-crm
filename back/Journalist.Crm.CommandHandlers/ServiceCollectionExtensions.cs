using Journalist.Crm.CommandHandlers.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Journalist.Crm.CommandHandlers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssembly(typeof(CreateClientHandler).Assembly);
            });

            return services;
        }
    }
}
