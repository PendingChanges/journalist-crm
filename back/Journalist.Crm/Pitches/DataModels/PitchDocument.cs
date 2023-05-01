using Journalist.Crm.Domain.Common;
using System;

namespace Journalist.Crm.Domain.Pitches.DataModels
{
    public record PitchDocument(EntityId Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId, OwnerId OwnerId);
}
