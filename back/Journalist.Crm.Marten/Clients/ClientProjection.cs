using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Clients.Events;
using Marten;
using Marten.Events.Projections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten.Clients
{
    public class ClientProjection : EventProjection
    {
        public ClientDocument Create(ClientCreated clientCreated)
            => new ClientDocument(clientCreated.Id, clientCreated.Name, clientCreated.OwnerId, new List<string>());

        public void Project(ClientDeleted @event, IDocumentOperations ops)
            => ops.Delete<ClientDocument>(@event.Id);

        public async Task Project(ClientRenamed @event, IDocumentOperations ops)
        {
            var client = await ops.Query<ClientDocument>().SingleAsync(c => c.Id == @event.Id);

            if (client != null)
            {
                var clientUpdated = client with { Name = @event.NewName };

                ops.Store(clientUpdated);
            }
        }
    }
}
