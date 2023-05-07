using System;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;

namespace Journalist.Crm.GraphQL.Pitches
{
    public record ModifyPitch(string Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId);
}
