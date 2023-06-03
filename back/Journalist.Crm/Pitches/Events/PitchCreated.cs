using System;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Pitches.Events
{
    public sealed record PitchCreated(EntityId Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId, OwnerId OwnerId);
}
