using Journalist.Crm.Domain.Pitches.Events;
using System;

namespace Journalist.Crm.Domain.Pitches
{
    public class PitchAggregate : AggregateBase
    {
        public string Title { get; private set; }
        public string? Content { get; private set; }
        public DateTime? DeadLineDate { get; private set; }
        public DateTime? IssueDate { get; private set; }
        public string ClientId { get; private set; }
        public string IdeaId { get; private set; }
        public string OwnerId { get; private set; }
        public bool Deleted { get; private set; }


        public PitchAggregate(string title, string? content, DateTime? deadLineDate, DateTime? issueDate, string clientId, string ideaId, string ownerId)
        {
            var id = Guid.NewGuid().ToString();

            var @event = new PitchCreated(id, title, content, deadLineDate, issueDate, clientId, ideaId, ownerId);

            Apply(@event);
            AddUncommitedEvent(@event);
        }

        public void Delete(string pitchId, string ownerId)
        {
            if (string.CompareOrdinal(Id, pitchId) != 0)
            {
                AddUncommitedError(new Error("INVALID_PITCH_ID", "The pitch id is invalid"));
            }

            if (string.CompareOrdinal(OwnerId, ownerId) != 0)
            {
                AddUncommitedError(new Error("NOT_PITCH_OWNER", "The user is not the owner of this pitch"));
            }

            if (HasErrors)
            {
                return;
            }

            var @event = new PitchDeleted(Id, ClientId, IdeaId);
            Apply(@event);
            AddUncommitedEvent(@event);
        }

        private void Apply(PitchCreated @event)
        {
            Id = @event.Id;
            Title = @event.Title;
            Content = @event.Content;
            DeadLineDate = @event.DeadLineDate;
            IssueDate = @event.IssueDate;
            ClientId = @event.ClientId;
            IdeaId = @event.IdeaId;
            OwnerId = @event.OwnerId;
            Deleted = false;

            Version++;
        }

        private void Apply(PitchDeleted @event)
        {
            Deleted = true;
        }
    }
}
