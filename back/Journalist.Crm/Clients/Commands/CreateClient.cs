using MediatR;

namespace Journalist.Crm.Domain.Clients.Commands
{
    public record CreateClient(string Name) : ICommand;
}
