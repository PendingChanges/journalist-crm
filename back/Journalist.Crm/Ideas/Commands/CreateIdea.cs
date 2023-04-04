using MediatR;

namespace Journalist.Crm.Domain.Ideas.Commands
{
    public record CreateIdea(string Name, string? Description, string OwnerId) : IRequest<IdeaAggregate>;
}
