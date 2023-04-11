using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class CreateClientHandler : SingleAggregateCommandHandlerBase<CreateClient, ClientAggregate>
    {
        public CreateClientHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(ClientAggregate aggregate, CreateClient command, string ownerId)
        {
        }

        protected override Task<ClientAggregate?> LoadAggregate(CreateClient command, string ownerId, CancellationToken cancellationToken)
            => Task.FromResult<ClientAggregate?>(new ClientAggregate(command.Name, ownerId));
    }
}
