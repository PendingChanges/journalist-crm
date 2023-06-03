using Journalist.Crm.Domain.Clients.DataModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Clients
{
    public interface IReadClients
    {
        Task<IReadOnlyList<ClientDocument>> AutoCompleteClientAsync(string text, string userId, CancellationToken cancellationToken);
        Task<ClientDocument?> GetClientAsync(string clientId, string userId, CancellationToken cancellationToken);
        Task<ClientResultSet> GetClientsAsync(GetClientsRequest request, CancellationToken cancellationToken = default);
    }
}
