using MediatR;
using System;

namespace Journalist.Crm.Domain.Pitches.Commands
{
    public record CreatePitch(string Title, string? Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId, string OwnerId) : IRequest<PitchAggregate>;
}
