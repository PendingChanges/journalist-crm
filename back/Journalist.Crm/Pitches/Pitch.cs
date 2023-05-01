using Journalist.Crm.Domain.Pitches.Events;
using System;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Pitches
{
    public class Pitch : Aggregate
    {
        public PitchContent Content { get; private set; }
        public DateTime? DeadLineDate { get; private set; }
        public DateTime? IssueDate { get; private set; }
        public string ClientId { get; private set; }
        public string IdeaId { get; private set; }
        public OwnerId OwnerId { get; private set; }
        public bool Deleted { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Pitch(PitchContent content, DateTime? deadLineDate, DateTime? issueDate, string clientId, string ideaId, OwnerId ownerId)
        {
            var id = EntityId.NewEntityId();

            var @event = new PitchCreated(id, content, deadLineDate, issueDate, clientId, ideaId, ownerId);

            Apply(@event);
            AddUncommittedEvent(@event);
        }

        private Pitch() { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public void Delete(OwnerId ownerId)
        {
            if (OwnerId != ownerId)
            {
                AddUncommittedError(new Error("NOT_PITCH_OWNER", "The user is not the owner of this pitch"));
            }

            if (HasErrors)
            {
                return;
            }

            var @event = new PitchDeleted(Id, ClientId, IdeaId);
            Apply(@event);
            AddUncommittedEvent(@event);
        }

        public void Modify(PitchContent content, DateTime? deadLineDate, DateTime? issueDate, string clientId, string ideaId, OwnerId ownerId)
        {
            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommittedError(new Error("NOT_PITCH_OWNER", "The user is not the owner of this pitch"));
            }

            if (HasErrors)
            {
                return;
            }

            if (content != Content)
            {
                var @event = new PitchContentChanged(Id, content);
                Apply(@event);
                AddUncommittedEvent(@event);
            }

            if (deadLineDate != DeadLineDate)
            {
                var @event = new PitchDeadLineRescheduled(Id, deadLineDate);
                Apply(@event);
                AddUncommittedEvent(@event);
            }

            if (issueDate != IssueDate)
            {
                var @event = new PitchIssueRescheduled(Id, issueDate);
                Apply(@event);
                AddUncommittedEvent(@event);
            }

            if (clientId != ClientId)
            {
                var @event = new PitchClientChanged(Id, clientId);
                Apply(@event);
                AddUncommittedEvent(@event);
            }

            if (ideaId != IdeaId)
            {
                var @event = new PitchIdeaChanged(Id, ideaId);
                Apply(@event);
                AddUncommittedEvent(@event);
            }
        }

        private void Apply(PitchContentChanged @event)
        {
            Content = @event.Content;

            IncrementVersion();
        }

        private void Apply(PitchCreated @event)
        {
            SetId(@event.Id);
            Content = @event.Content;
            DeadLineDate = @event.DeadLineDate;
            IssueDate = @event.IssueDate;
            ClientId = @event.ClientId;
            IdeaId = @event.IdeaId;
            OwnerId = @event.OwnerId;
            Deleted = false;

            IncrementVersion();
        }

        private void Apply(PitchDeleted @event)
        {
            Deleted = true;
            IncrementVersion();
        }

        private void Apply(PitchDeadLineRescheduled @event)
        {
            DeadLineDate = @event.DeadLineDate;
            IncrementVersion();
        }

        private void Apply(PitchIssueRescheduled @event)
        {
            IssueDate = @event.IssueDate;
            IncrementVersion();
        }

        private void Apply(PitchClientChanged @event)
        {
            ClientId = @event.ClientId;
            IncrementVersion();
        }

        private void Apply(PitchIdeaChanged @event)
        {
            IdeaId = @event.IdeaId;
            IncrementVersion();
        }
    }
}
