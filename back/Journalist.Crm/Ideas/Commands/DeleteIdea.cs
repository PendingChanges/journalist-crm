using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Ideas.Commands
{
    public record DeleteIdea(EntityId Id): ICommand;
}
