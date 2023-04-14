using System;
using Journalist.Crm.Domain.Pitches.ValueObjects;

namespace Journalist.Crm.Domain.Pitches.Events
{
    public sealed record PitchCreated(string Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId, string OwnerId);
}
