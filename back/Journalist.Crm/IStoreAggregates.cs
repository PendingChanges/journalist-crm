using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain
{
    public interface IStoreAggregates
    {
        Task<T?> LoadAsync<T>(string id, int? version = null, CancellationToken ct = default) where T : Aggregate;
        Task StoreAsync(string aggregateId, long version, IEnumerable<object> events, CancellationToken ct = default);
    }
}