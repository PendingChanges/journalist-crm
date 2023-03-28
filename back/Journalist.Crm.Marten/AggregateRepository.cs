﻿using Journalist.Crm.Domain;
using Marten;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten
{
    public sealed class AggregateRepository : IStoreAggregates
    {
        private readonly IDocumentStore store;

        public AggregateRepository(IDocumentStore store)
        {
            this.store = store;
        }

        public async Task StoreAsync(AggregateBase aggregate, CancellationToken ct = default)
        {
            await using var session = store.OpenSession();
            // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
            var events = aggregate.GetUncommitedEvents().ToArray();
            session.Events.Append(aggregate.Id, aggregate.Version, events);
            await session.SaveChangesAsync(ct);
            // Once successfully persisted, clear events from list of uncommitted events
            aggregate.ClearUncommitedEvents();
        }

        public async Task<T> LoadAsync<T>(
            string id,
            int? version = null,
            CancellationToken ct = default
        ) where T : AggregateBase
        {
            await using var session = store.OpenSession();
            var aggregate = await session.Events.AggregateStreamAsync<T>(id, version ?? 0, token: ct);
            return aggregate ?? throw new InvalidOperationException($"No aggregate by id {id}.");
        }
    }
}