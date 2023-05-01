using Journalist.Crm.Domain.Ideas.Events;
using System;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Ideas
{
    public class Idea : Aggregate
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public OwnerId OwnerId { get; private set; }
        public bool Deleted { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Idea(string name, string? description, OwnerId ownerId)
        {
            var id = EntityId.NewEntityId();

            var @event = new IdeaCreated(id, name, description, ownerId);
            Apply(@event);
            AddUncommittedEvent(@event);
        }

        private Idea() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public void Delete(OwnerId ownerId)
        {
            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommittedError(new Error("NOT_IDEA_OWNER", "The user is not the owner of this idea"));
            }

            if (HasErrors)
            {
                return;
            }

            var @event = new IdeaDeleted(Id);
            Apply(@event);
            AddUncommittedEvent(@event);
        }

        public void Modify(string newName, string? newDescription, OwnerId ownerId)
        {
            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommittedError(new Error("NOT_IDEA_OWNER", "The user is not the owner of this idea"));
            }

            if (HasErrors)
            {
                return;
            }
            
            var @event = new IdeaModified(Id, newName, newDescription);
            Apply(@event);
            AddUncommittedEvent(@event);
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
