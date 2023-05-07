using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Ideas.Commands
{
    public record ModifyIdea(EntityId Id, string NewName, string? NewDescription) : ICommand;
}
