using System;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Pitches.Commands
{
    public record ModifyPitch(EntityId Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId) : ICommand;
}
