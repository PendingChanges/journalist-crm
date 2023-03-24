﻿using Journalist.Crm.Domain.Ideas.Events;
using System;

namespace Journalist.Crm.Domain.Ideas
{
    public class IdeaAggregate : AggregateBase
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string OwnerId { get; private set; }
        public bool Deleted { get; private set; }

        public IdeaAggregate(string name, string? description, string ownerId)
        {
            var id = Guid.NewGuid().ToString();

            var @event = new IdeaCreated(id, name, description, ownerId);

            Apply(@event);
            AddUncommitedEvent(@event);
        }

        public void Delete(string ideaId, string ownerId)
        {
            if (string.CompareOrdinal(Id, ideaId) != 0)
            {
                AddUncommitedError(new Error("INVALID_IDEA_ID", "The idea id is invalid"));
            }

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

        private void Apply(IdeaCreated @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Description = @event.Description;
            OwnerId = @event.OwnerId;
            Deleted = false;

            Version++;
        }

        private void Apply(IdeaDeleted @event)
        {
            Deleted = true;
        }
    }
}
