using MediatR;

namespace Journalist.Crm.Domain.Clients.Commands
{
    public record CreateClient(string Name, string OwnerId) : IRequest<AggregateResult<ClientAggregate>>;
}
