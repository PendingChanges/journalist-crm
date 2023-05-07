using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Ideas.Commands
{
    public record DeleteIdea(EntityId Id): ICommand;
}
