using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.CQRS
{
    public interface IWriteEvents
    {
        Task StoreAsync(string aggregateId, long version, IEnumerable<object> events, CancellationToken ct = default);
    }
}