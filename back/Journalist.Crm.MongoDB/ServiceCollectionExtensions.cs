using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.MongoDB.Clients;
using Journalist.Crm.MongoDB.Ideas;
using Journalist.Crm.MongoDB.Pitches;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journalist.Crm.MongoDB
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfigurationSection mongoDBSection)
        {
            services.Configure<MongoDBOptions>(mongoDBSection);

            services.AddTransient<IReadClients, ClientsRepository>()
                    .AddTransient<IWriteClients, ClientsRepository>()
                    .AddTransient<IReadIdeas, IdeasRepository>()
                    .AddTransient<IWriteIdeas, IdeasRepository>()
                    .AddTransient<IReadPitches, PitchesRepository>()
                    .AddTransient<IWritePitches, PitchesRepository>();

            return services;
        }

    }
}
