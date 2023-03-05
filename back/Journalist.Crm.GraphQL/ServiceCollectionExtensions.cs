using Journalist.Crm.GraphQL.Clients;
using Journalist.Crm.GraphQL.Ideas;
using Journalist.Crm.GraphQL.Pitches;
using Microsoft.Extensions.DependencyInjection;

namespace Journalist.Crm.GraphQL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJournalistGraphQL(this IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType(q => q.Name("Query"))
                            .AddType<IdeasQueries>()
                            .AddType<ClientsQueries>()
                            .AddType<PitchesQueries>()
                .AddMutationType(m => m.Name("Mutation"))
                            .AddType<ClientsMutations>()
                            .AddType<IdeasMutations>()
                            .AddType<PitchesMutations>()
                .AddTypeExtension<ClientExtensions>()
                .AddTypeExtension<IdeaExtensions>()
                .AddTypeExtension<PitchExtensions>();

            services.AddErrorFilter<GraphQLErrorFilter>();

            return services;
        }
    }
}
