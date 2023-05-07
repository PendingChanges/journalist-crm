using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Clients.Commands
{
    public record RenameClient(EntityId Id, string NewName) : ICommand;
}
