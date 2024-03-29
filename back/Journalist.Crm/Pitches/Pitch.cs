﻿using Journalist.Crm.Domain.Pitches.Events;
using System;
using Journalist.Crm.Domain.ValueObjects;
using Journalist.Crm.Domain.CQRS;

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

        private PitchStateMachine _stateMachine;

        public string CurrentState => _stateMachine.CurrentState;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Pitch() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public AggregateResult Create(PitchContent content, DateTime? deadLineDate, DateTime? issueDate,
            string clientId, string ideaId, OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            var id = EntityId.NewEntityId();

            var @event = new PitchCreated(id, content, deadLineDate, issueDate, clientId, ideaId, ownerId);

            Apply(@event);
            result.AddEvent(@event);

            return result;
        }

        public AggregateResult Cancel(OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotPitchOwner);
            result.CheckAndAddError(() => !_stateMachine.CanCancel(), ErrorCollection.WellKnownErrors.PitchNotCancellable);

            if (result.HasErrors)
            {
                return result;
            }

            var @event = new PitchCancelled(Id, ClientId, IdeaId);
            Apply(@event);
            result.AddEvent(@event);

            return result;
        }

        public AggregateResult Validate(OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotPitchOwner);
            result.CheckAndAddError(() => !_stateMachine.CanValidate(), ErrorCollection.WellKnownErrors.PitchNotValidatable);

            if (result.HasErrors)
            {
                return result;
            }

            var @event = new PitchReadyToSend(Id);
            Apply(@event);
            result.AddEvent(@event);

            return result;
        }

        public AggregateResult Send(OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotPitchOwner);
            result.CheckAndAddError(() => !_stateMachine.CanSend(), ErrorCollection.WellKnownErrors.PitchNotSendable);

            if (result.HasErrors)
            {
                return result;
            }

            var @event = new PitchSent(Id);
            Apply(@event);
            result.AddEvent(@event);

            return result;
        }

        public AggregateResult Accept(OwnerId ownerId)
        {
            var result = AggregateResult.Create();
            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotPitchOwner);
            result.CheckAndAddError(() => !_stateMachine.CanAccept(), ErrorCollection.WellKnownErrors.PitchNotAcceptable);
            if (result.HasErrors)
            {
                return result;
            }
            var @event = new PitchAccepted(Id);
            Apply(@event);
            result.AddEvent(@event);
            return result;
        }

        public AggregateResult Refuse(OwnerId ownerId)
        {
            var result = AggregateResult.Create();
            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotPitchOwner);
            result.CheckAndAddError(() => !_stateMachine.CanRefuse(), ErrorCollection.WellKnownErrors.PitchNotRefusable);
            if (result.HasErrors)
            {
                return result;
            }
            var @event = new PitchRefused(Id);
            Apply(@event);
            result.AddEvent(@event);
            return result;
        }   

        public AggregateResult Modify(PitchContent content, DateTime? deadLineDate, DateTime? issueDate, string clientId, string ideaId, OwnerId ownerId)
        {
            var result = AggregateResult.Create();

            result.CheckAndAddError(() => OwnerId != ownerId, ErrorCollection.WellKnownErrors.NotPitchOwner);
            result.CheckAndAddError(() => !_stateMachine.CanModify(), ErrorCollection.WellKnownErrors.PitchNotModifiable);

            if (result.HasErrors)
            {
                return result;
            }

            if (content != Content)
            {
                var @event = new PitchContentChanged(Id, content);
                Apply(@event);
                result.AddEvent(@event);
            }

            if (deadLineDate != DeadLineDate)
            {
                var @event = new PitchDeadLineRescheduled(Id, deadLineDate);
                Apply(@event);
                result.AddEvent(@event);
            }

            if (issueDate != IssueDate)
            {
                var @event = new PitchIssueRescheduled(Id, issueDate);
                Apply(@event);
                result.AddEvent(@event);
            }

            if (clientId != ClientId)
            {
                var @event = new PitchClientChanged(Id, clientId);
                Apply(@event);
                result.AddEvent(@event);
            }

            if (ideaId != IdeaId)
            {
                var @event = new PitchIdeaChanged(Id, ideaId);
                Apply(@event);
                result.AddEvent(@event);
            }

            return result;
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
            _stateMachine = new PitchStateMachine(PitchStates.Draft);

            IncrementVersion();
        }

        private void Apply(PitchCancelled @event)
        {
            _stateMachine.SetStatus(PitchStates.Cancelled);
            IncrementVersion();
        }

        private void Apply(PitchSent @event)
        {
            _stateMachine.SetStatus(PitchStates.Sent);
            IncrementVersion();
        }

        private void Apply(PitchAccepted @event)
        {
            _stateMachine.SetStatus(PitchStates.Accepted);
            IncrementVersion();
        }

        private void Apply(PitchRefused @event)
        {
            _stateMachine.SetStatus(PitchStates.Refused);
            IncrementVersion();
        }

        private void Apply(PitchReadyToSend @event)
        {
            _stateMachine.SetStatus(PitchStates.ReadyToSend);
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

        public bool CanValidate() => _stateMachine.CanValidate();
        public bool CanModify() => _stateMachine.CanModify();
        public bool CanCancel() => _stateMachine.CanCancel();
        public bool CanSend() => _stateMachine.CanSend();
        public bool CanAccept() => _stateMachine.CanAccept();
        public bool CanRefuse() => _stateMachine.CanRefuse();
    }
}
