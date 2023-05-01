using System;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Pitches.Events
{
    public sealed record PitchCreated(EntityId Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId, OwnerId OwnerId);
}
