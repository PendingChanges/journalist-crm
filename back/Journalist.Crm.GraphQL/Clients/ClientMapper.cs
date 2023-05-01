using Journalist.Crm.Domain.Clients.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Journalist.Crm.GraphQL.Clients
{
    public static class ClientMapper
    {
        public static Client? ToClientOrNull(this ClientDocument? clientDocument)
            => clientDocument == null ? null : clientDocument.ToClient();

        public static Client ToClient(this ClientDocument clientDocument)
            => new Client(clientDocument.Id, clientDocument.Name, clientDocument.OwnerId, clientDocument.PitchesIds.Count);

        public static IReadOnlyList<Client> ToClients(this IReadOnlyList<ClientDocument> clients)
            => clients.Select(ToClient).ToList();
    }
}
