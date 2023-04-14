using System;
using Journalist.Crm.Domain.Pitches.ValueObjects;

namespace Journalist.Crm.Domain.Pitches.Commands
{
    public record ModifyPitch(string Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId) : ICommand;
}
