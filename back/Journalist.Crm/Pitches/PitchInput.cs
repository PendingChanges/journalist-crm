using System;

namespace Journalist.Crm.Domain.Pitches
{
    public record PitchInput(string Title, string? Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId);
}
