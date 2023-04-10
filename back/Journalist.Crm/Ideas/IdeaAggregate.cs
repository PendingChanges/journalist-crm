using Journalist.Crm.Domain.Ideas.Events;
using System;

namespace Journalist.Crm.Domain.Ideas
{
    public class IdeaAggregate : AggregateBase
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string OwnerId { get; private set; }
        public bool Deleted { get; private set; }

        public IdeaAggregate()
        {
            Name = string.Empty;
            Description = string.Empty;
            OwnerId = string.Empty;
            Deleted = false;
        }

        public void Create(string name, string? description, string ownerId)
        {
            if(State == AggregateState.Set)
            {
                AddUncommitedError(new Error("AGGREGATE_ALREADY_SET", "The aggregate is already set"));
            }

            if (HasErrors)
            {
                return;
            }

            var id = Guid.NewGuid().ToString();

            var @event = new IdeaCreated(id, name, description, ownerId);
            Apply(@event);
            AddUncommitedEvent(@event);
        }

        public void Delete(string ownerId)
        {
            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommitedError(new Error("NOT_IDEA_OWNER", "The user is not the owner of this idea"));
            }

            if (HasErrors)
            {
                return;
            }

            var @event = new IdeaDeleted(Id);
            Apply(@event);
            AddUncommitedEvent(@event);
        }

        public void Modify(string newName, string newDescrition, string ownerId)
        {
            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommitedError(new Error("NOT_IDEA_OWNER", "The user is not the owner of this idea"));
            }

            if (HasErrors)
            {
                return;
            }
            
            var @event = new IdeaModified(Id, newName, newDescrition);
            Apply(@event);
            AddUncommitedEvent(@event);
        }

        private void Apply(IdeaCreated @event)
        {
            SetId(@event.Id);
            Activate();
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
