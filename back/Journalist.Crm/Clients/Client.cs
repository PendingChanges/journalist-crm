using Journalist.Crm.Domain.Clients.Events;
using System;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Clients
{
    public sealed class Client : Aggregate
    {
        public string Name { get; private set; }
        public OwnerId OwnerId { get; private set; }
        public bool Deleted { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Client(string name, OwnerId ownerId)
        {
            var id = EntityId.NewEntityId();

            var @event = new ClientCreated(id, name, ownerId);

            Apply(@event);
            AddUncommittedEvent(@event);
        }

        private Client() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public void Delete(OwnerId ownerId)
        {
            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommittedError(new Error("NOT_CLIENT_OWNER", "The user is not the owner of this client"));
            }

            if (HasErrors)
            {
                return;
            }

            var @event = new ClientDeleted(Id);
            Apply(@event);
            AddUncommittedEvent(@event);
        }

        public void Rename(string newName, OwnerId ownerId)
        {
            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommittedError(new Error("NOT_CLIENT_OWNER", "The user is not the owner of this client"));
            }

            if (HasErrors)
            {
                return;
            }

            if (string.CompareOrdinal(Name, newName) == 0)
            {
                return;
            }

            var @event = new ClientRenamed(Id, newName);
            Apply(@event);
            AddUncommittedEvent(@event);
        }

        private void Apply(ClientRenamed @event)
        {
            Name = @event.NewName;
            IncrementVersion();
        }

        private void Apply(ClientCreated @event)
        {
            SetId(@event.Id);

            Name = @event.Name;
            OwnerId = @event.OwnerId;
            Deleted = false;

            IncrementVersion();
        }

        private void Apply(ClientDeleted @event)
        {
            Deleted = true;
            IncrementVersion();
        }
    }
}
