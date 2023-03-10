using System;

namespace Journalist.Crm.Domain.Pitches.DataModels
{
    public record Pitch(string Id, string Title, string? Content, DateTime? DeadLineDate, DateTime? IssueDate, string StatusCode, string ClientId, string IdeaId, string UserId);
}
