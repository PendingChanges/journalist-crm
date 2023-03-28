using Journalist.Crm.Domain.Clients.Events;
using System;

namespace Journalist.Crm.Domain.Clients
{
    public sealed class ClientAggregate : AggregateBase
    {
        public string Name { get; private set; }
        public string OwnerId { get; private set; }
        public bool Deleted { get; private set; }

        public ClientAggregate(string name, string ownerId)
        {
            var id = Guid.NewGuid().ToString();

            var @event = new ClientCreated(id, name, ownerId);

            Apply(@event);
            AddUncommitedEvent(@event);
        }

        private ClientAggregate() { }

        public void Delete(string clientId, string ownerId)
        {
            if(string.CompareOrdinal(Id, clientId) != 0)
            {
                AddUncommitedError(new Error("INVALID_CLIENT_ID", "The client id is invalid"));
            }

            if(string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommitedError(new Error("NOT_CLIENT_OWNER", "The user is not the owner of this client"));
            }

            if (HasErrors)
            {
                return;
            }

            var @event = new ClientDeleted(Id);
            Apply(@event);
            AddUncommitedEvent(@event);
        }

        private void Apply(ClientCreated @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            OwnerId = @event.OwnerId;
            Deleted = false;

            Version++;
        }

        private void Apply(ClientDeleted @event)
        {
            Deleted = true;
            Version++;
        }
    }
}
