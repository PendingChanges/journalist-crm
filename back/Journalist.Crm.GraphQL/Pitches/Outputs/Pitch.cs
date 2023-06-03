using Journalist.Crm.Domain.Pitches;
using System;

namespace Journalist.Crm.GraphQL.Pitches.Outputs
{
    public record Pitch(string Id,
        PitchContent Content,
        DateTime? DeadLineDate,
        DateTime? IssueDate,
        string ClientId,
        string IdeaId,
        string UserId);
}
