using MediatR;

namespace Journalist.Crm.Domain.Ideas.Commands
{
    public record DeleteIdea(string Id, string OwnerId) : IRequest<IdeaAggregate>;
}
