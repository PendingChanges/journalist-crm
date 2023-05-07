using Journalist.Crm.Domain;
using Marten;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten
{
    public sealed class AggregateRepository : IStoreAggregates
    {
        private readonly IDocumentStore _store;

        public AggregateRepository(IDocumentStore store)
        {
            this._store = store;
        }

        public async Task StoreAsync(string aggregateId, long version, IEnumerable<object> events, CancellationToken ct = default)
        {
            await using var session = _store.LightweightSession();

            session.Events.Append(aggregateId, version, events);

            await session.SaveChangesAsync(ct);
        }

        public async Task<T?> LoadAsync<T>(
            string id,
            int? version = null,
            CancellationToken ct = default
        ) where T : Aggregate
        {
            await using var session = _store.LightweightSession();
            var aggregate = await session.Events.AggregateStreamAsync<T>(id, version ?? 0, token: ct);
            return aggregate;
        }
    }
}