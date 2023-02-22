using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Neo4j.Clients;
using Journalist.Crm.Neo4j.Ideas;
using Journalist.Crm.Neo4j.Pitches;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;

namespace Journalist.Crm.Neo4j
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNeo4j(this IServiceCollection services, IConfigurationSection neo4jSection)
        {
            var neo4jOptions = new Neo4jOptions();
            neo4jSection.Bind(neo4jOptions);

            IDriver driver = GraphDatabase.Driver(
              neo4jOptions.BoltUrl,
               AuthTokens.Basic(neo4jOptions.Username, neo4jOptions.Password));

            services
                .AddSingleton(driver)
                .AddTransient<IWriteClients, ClientsRepository>()
                .AddTransient<IReadClients, ClientsRepository>()
                .AddTransient<IWriteIdeas, IdeasRepository>()
                .AddTransient<IReadIdeas, IdeasRepository>()
                .AddTransient<IReadPitches, PitchesRepository>();

            return services;
        }
    }
}
