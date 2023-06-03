using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Ideas.Commands
{
    public record ModifyIdea(EntityId Id, string NewName, string? NewDescription) : ICommand;
}
