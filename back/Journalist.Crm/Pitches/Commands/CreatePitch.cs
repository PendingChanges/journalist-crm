using Journalist.Crm.Domain.Pitches.ValueObjects;
using MediatR;
using System;

namespace Journalist.Crm.Domain.Pitches.Commands
{
    public record CreatePitch(PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId) : ICommand;
}
