using System;

namespace Journalist.Crm.Domain.Pitches.DataModels
{
    public record PitchDocument(string Id, string Title, string? Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId, string UserId);
}
