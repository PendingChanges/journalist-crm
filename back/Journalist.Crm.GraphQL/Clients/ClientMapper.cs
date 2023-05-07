using Journalist.Crm.Domain.Clients.DataModels;
using System.Collections.Generic;
using System.Linq;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.GraphQL.Clients
{
    public static class ClientMapper
    {
        public static Client? ToClientOrNull(this ClientDocument? clientDocument)
            => clientDocument?.ToClient();

        public static Client ToClient(this ClientDocument clientDocument)
            => new(clientDocument.Id, clientDocument.Name, clientDocument.OwnerId, clientDocument.PitchesIds.Count);

        public static IReadOnlyList<Client> ToClients(this IReadOnlyList<ClientDocument> clients)
            => clients.Select(ToClient).ToList();

        public static Domain.Clients.Commands.RenameClient ToCommand(this RenameClient renameClient)
            => new(new EntityId(renameClient.Id), renameClient.NewName);  

        public static Domain.Clients.Commands.DeleteClient ToCommand(this DeleteClient deleteClient)
            => new(new EntityId(deleteClient.Id));

        public static Domain.Clients.Commands.CreateClient ToCommand(this CreateClient createClient)
            => new(createClient.Name);
    }
}
