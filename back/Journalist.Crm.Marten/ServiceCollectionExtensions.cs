using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using Journalist.Crm.Marten.Clients;
using Journalist.Crm.Marten.Ideas;
using Journalist.Crm.Marten.Pitches;
using Marten;
using Marten.Events;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journalist.Crm.Marten
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJournalistMarten(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddMarten(options =>
            {
                options.Connection(configuration.GetConnectionString("Marten"));
                
                // Events
                options.Events.StreamIdentity = StreamIdentity.AsString;

                // Projections
                options.Projections.Add(new ClientProjection(), ProjectionLifecycle.Inline);
                options.Projections.Add(new IdeaProjection(), ProjectionLifecycle.Inline);
                options.Projections.Add(new PitchProjection(), ProjectionLifecycle.Inline);

                // Indexes
                options.Schema.For<ClientDocument>().UniqueIndex(c => c.Id);
                options.Schema.For<ClientDocument>().Index(c => c.UserId);
                options.Schema.For<ClientDocument>().FullTextIndex(c => c.Name);

                options.Schema.For<PitchDocument>().UniqueIndex(p => p.Id);
                options.Schema.For<PitchDocument>().Index(p => p.UserId);
                options.Schema.For<PitchDocument>().Index(p => p.ClientId);
                options.Schema.For<PitchDocument>().Index(p => p.IdeaId);

                options.Schema.For<IdeaDocument>().UniqueIndex(c => c.Id);
                options.Schema.For<IdeaDocument>().Index(c => c.UserId);
                options.Schema.For<IdeaDocument>().FullTextIndex(c => c.Name);
            })
            .AddAsyncDaemon(DaemonMode.Solo);

            services.AddTransient<IStoreAggregates, AggregateRepository>();
            services.AddTransient<IReadClients, ClientRepository>();
            services.AddTransient<IReadIdeas, IdeaRepository>();
            services.AddTransient<IReadPitches, PitchRepository>();

            return services;
        }
    }
}
