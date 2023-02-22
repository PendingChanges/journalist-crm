using Journalist.Crm.Domain.Clients.DataModels;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Clients
{
    public interface IReadClients
    {
        Task<ClientResultSet> GetClientsAsync(GetClientsRequest request, CancellationToken cancellationToken = default);
    }
}
