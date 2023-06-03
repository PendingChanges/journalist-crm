using System;
using Journalist.Crm.Domain.Pitches;

namespace Journalist.Crm.GraphQL.Pitches.Inputs
{
    public record CreatePitch(PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId);
}
