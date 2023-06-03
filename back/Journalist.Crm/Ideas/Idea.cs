using Journalist.Crm.Domain.Ideas.Events;
using Journalist.Crm.Domain.ValueObjects;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.Domain.Ideas
{
    public class Idea : Aggregate
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public OwnerId OwnerId { get; private set; }
        public bool Deleted { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Idea() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public AggregateResult Create(string name, string? description, OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            var id = EntityId.NewEntityId();

            var @event = new IdeaCreated(id, name, description, ownerId);
            Apply(@event);
            result.AddEvent(@event);

            return result;
        }

        public AggregateResult Delete(OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotIdeaOwner);

            if (result.HasErrors)
            {
                return result;
            }

            var @event = new IdeaDeleted(Id);
            Apply(@event);
            result.AddEvent(@event);

            return result;
        }

        public AggregateResult Modify(string newName, string? newDescription, OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotIdeaOwner);

            if (result.HasErrors)
            {
                return result;
            }

            var @event = new IdeaModified(Id, newName, newDescription);
            Apply(@event);
            result.AddEvent(@event);

            return result;
        }

        private void Apply(IdeaCreated @event)
        {
            SetId(@event.Id);
            Name = @event.Name;
            Description = @event.Description;
            OwnerId = @event.OwnerId;
            Deleted = false;

            IncrementVersion();
        }

        private void Apply(IdeaModified @event)
        {
            Name = @event.NewName;
            Description = @event.NewDescription;

            IncrementVersion();
        }

        private void Apply(IdeaDeleted @event)
        {
            Deleted = true;
        }
    }
}
