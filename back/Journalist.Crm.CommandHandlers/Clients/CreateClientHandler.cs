using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class CreateClientHandler : SingleAggregateCommandHandler<CreateClient, Client>
    {
        public CreateClientHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(Client aggregate, CreateClient command, OwnerId ownerId)
        {
        }

        protected override Task<Client?> LoadAggregate(CreateClient command, OwnerId ownerId, CancellationToken cancellationToken)
            => Task.FromResult<Client?>(new Client(command.Name, ownerId));
    }
}
