using Journalist.Crm.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten
{
    public interface IStoreAggregates
    {
        Task<T> LoadAsync<T>(string id, int? version = null, CancellationToken ct = default) where T : AggregateBase;
        Task StoreAsync(AggregateBase aggregate, CancellationToken ct = default);
    }
}