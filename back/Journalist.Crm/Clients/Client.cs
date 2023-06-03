using Journalist.Crm.Domain.Clients.Events;
using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Clients
{
    public sealed class Client : Aggregate
    {
        public string Name { get; private set; }
        public OwnerId OwnerId { get; private set; }
        public bool Deleted { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Client() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public AggregateResult Create(string name, OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            var id = EntityId.NewEntityId();

            var @event = new ClientCreated(id, name, ownerId);

            Apply(@event);
            result.AddEvent(@event);

            return result;
        }

        public AggregateResult Delete(OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotClientOwner);

            if (!result.HasErrors)
            {
                var @event = new ClientDeleted(Id);
                Apply(@event);
                result.AddEvent(@event);
            }

            return result;
        }

        public AggregateResult Rename(string newName, OwnerId ownerId)
        {
            var result = AggregateResult.Create();
            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotClientOwner);

            if (result.HasErrors)
            {
                return result;
            }

            if (string.CompareOrdinal(Name, newName) == 0)
            {
                return result;
            }

            var @event = new ClientRenamed(Id, newName);
            Apply(@event);
            result.AddEvent(@event);

            return result;
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
