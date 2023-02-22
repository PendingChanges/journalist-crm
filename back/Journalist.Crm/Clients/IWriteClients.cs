using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Clients;

public interface IWriteClients
{
    Task<string> AddClientAsync(ClientInput input, string userId, CancellationToken cancellationToken = default);
    Task RemoveClientAsync(string id, string userId, CancellationToken cancellationToken = default);
}