using System;
using Journalist.Crm.Domain.Pitches;

namespace Journalist.Crm.GraphQL.Pitches
{
    public record CreatePitch(PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId);
}
