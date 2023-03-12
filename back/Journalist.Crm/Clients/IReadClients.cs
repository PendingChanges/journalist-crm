using Journalist.Crm.Domain.Clients.DataModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Clients
{
    public interface IReadClients
    {
        Task<IEnumerable<Client>> AutoCompleteClientasync(string text, string userId, CancellationToken cancellationToken);
        Task<Client?> GetClientAsync(string clientId, string userId, CancellationToken cancellationToken);
        Task<ClientResultSet> GetClientsAsync(GetClientsRequest request, CancellationToken cancellationToken = default);
    }
}
