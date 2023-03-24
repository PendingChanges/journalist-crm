using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Clients.Events;
using Marten;
using Marten.Events.Projections;
using System.Collections.Generic;

namespace Journalist.Crm.Marten.Clients
{
    public class ClientProjection : EventProjection
    {
        public ClientDocument Create(ClientCreated clientCreated)
            => new ClientDocument(clientCreated.Id, clientCreated.Name, clientCreated.OwnerId, new List<string>());

        public void Project(ClientDeleted clientDeleted, IDocumentOperations ops)
            => ops.Delete<ClientDocument>(clientDeleted.Id);
    }
}
