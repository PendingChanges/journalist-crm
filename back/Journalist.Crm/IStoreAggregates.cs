using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain
{
    public interface IStoreAggregates
    {
        Task<T?> LoadAsync<T>(string id, int? version = null, CancellationToken ct = default) where T : AggregateBase;
        Task StoreAsync(AggregateBase aggregate, CancellationToken ct = default);
    }
}