using MediatR;

namespace Journalist.Crm.Domain.Clients.Commands
{
    public record DeleteClient(string Id) : ICommand;
}
