﻿using System;
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
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.Marten
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJournalistMarten(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddMarten(options =>
            {
                options.Connection(configuration.GetConnectionString("Marten") ?? throw new ArgumentException(("missing connection string")));

                // Events
                options.Events.StreamIdentity = StreamIdentity.AsString;

                // Projections
                options.Projections.Add(new ClientProjection(), ProjectionLifecycle.Inline);
                options.Projections.Add(new IdeaProjection(), ProjectionLifecycle.Inline);
                options.Projections.Add(new PitchProjection(), ProjectionLifecycle.Inline);

                // Indexes
                options.Schema.For<ClientDocument>().UniqueIndex(c => c.Id);
                options.Schema.For<ClientDocument>().Index(c => c.OwnerId);
                options.Schema.For<ClientDocument>().FullTextIndex(c => c.Name);

                options.Schema.For<PitchDocument>().UniqueIndex(p => p.Id);
                options.Schema.For<PitchDocument>().Index(p => p.OwnerId);
                options.Schema.For<PitchDocument>().Index(p => p.ClientId);
                options.Schema.For<PitchDocument>().Index(p => p.IdeaId);

                options.Schema.For<IdeaDocument>().UniqueIndex(c => c.Id);
                options.Schema.For<IdeaDocument>().Index(c => c.OwnerId);
                options.Schema.For<IdeaDocument>().FullTextIndex(c => c.Name);
            });

            var querySessionDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IQuerySession));

            if (querySessionDescriptor != null)
            {
                services.Remove(querySessionDescriptor);
            }
            services.AddTransient(s => s.GetRequiredService<ISessionFactory>().QuerySession());

            var documentSessionDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IDocumentSession));

            if (documentSessionDescriptor != null)
            {
                services.Remove(documentSessionDescriptor);
            }
            services.AddTransient(s => s.GetRequiredService<ISessionFactory>().OpenSession());

            services.AddTransient<AggregateRepository>();
            services.AddTransient<IWriteEvents>(sp => sp.GetRequiredService<AggregateRepository>());
            services.AddTransient<IReadAggregates>(sp => sp.GetRequiredService<AggregateRepository>());
            services.AddTransient<IReadClients, ClientRepository>();
            services.AddTransient<IReadIdeas, IdeaRepository>();
            services.AddTransient<IReadPitches, PitchRepository>();

            return services;
        }
    }
}
