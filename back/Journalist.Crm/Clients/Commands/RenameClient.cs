using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Clients.Commands
{
    public record RenameClient(EntityId Id, string NewName) : ICommand;
}
