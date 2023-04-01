using Journalist.Crm.Domain.Clients.Events;
using System;

namespace Journalist.Crm.Domain.Clients
{
    public sealed class ClientAggregate : AggregateBase
    {
        public string Name { get; private set; }
        public string OwnerId { get; private set; }
        public bool Deleted { get; private set; }

        public ClientAggregate()
        {
            Name = string.Empty;
            OwnerId = string.Empty;
            Deleted = false;
        }

        public void Create(string name, string ownerId)
        {
            if (State == AggregateState.Set)
            {
                AddUncommitedError(new Error("AGGREGATE_ALREADY_SET", "The aggregate is already set"));
            }

            if (HasErrors)
            {
                return;
            }

            var id = Guid.NewGuid().ToString();

            var @event = new ClientCreated(id, name, ownerId);

            Apply(@event);
            AddUncommitedEvent(@event);
        }

        public void Delete(string ownerId)
        {
            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
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

        public void Rename(string newName, string ownerId)
        {
            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommitedError(new Error("NOT_CLIENT_OWNER", "The user is not the owner of this client"));
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
            AddUncommitedEvent(@event);
        }

        private void Apply(ClientRenamed @event)
        {
            Name = @event.NewName;
            IncrementVersion();
        }

        private void Apply(ClientCreated @event)
        {
            SetId(@event.Id);
            Activate();
            Name = @event.Name;
            OwnerId = @event.OwnerId;
            Deleted = false;

            IncrementVersion();
        }

        private void Apply(ClientDeleted _)
        {
            Deleted = true;
            IncrementVersion();
        }
    }
}
