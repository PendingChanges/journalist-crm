using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain
{
    public interface IStoreAggregates
    {
        Task<T?> LoadAsync<T>(string id, int? version = null, CancellationToken ct = default) where T : Aggregate;
        Task StoreAsync(Aggregate aggregate, CancellationToken ct = default);
    }
}