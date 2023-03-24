using MediatR;

namespace Journalist.Crm.Domain.Clients.Commands
{
    public record DeleteClient(string Id, string OwnerId) : IRequest<AggregateResult<ClientAggregate>>;
}
