using System;

namespace Journalist.Crm.Domain.Pitches.Events
{
    public sealed record PitchCreated(string Id, string Title, string? Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId, string OwnerId);
}
