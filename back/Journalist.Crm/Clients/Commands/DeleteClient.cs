using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Clients.Commands
{
    public record DeleteClient(EntityId Id) : ICommand;
}
