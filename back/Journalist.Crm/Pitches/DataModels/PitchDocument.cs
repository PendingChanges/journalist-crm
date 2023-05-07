using System;

namespace Journalist.Crm.Domain.Pitches.DataModels
{
    public record PitchDocument(string Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId, string OwnerId);
}
