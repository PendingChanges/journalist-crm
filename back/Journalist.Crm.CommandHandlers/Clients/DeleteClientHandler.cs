using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class DeleteClientHandler : SingleAggregateCommandHandlerBase<DeleteClient, ClientAggregate>
    {
        public DeleteClientHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(ClientAggregate aggregate, DeleteClient command, string ownerId)
            => aggregate.Delete(ownerId);

        protected override Task<ClientAggregate?> LoadAggregate(DeleteClient command, string ownerId, CancellationToken cancellationToken)
            => _aggregateStore.LoadAsync<ClientAggregate>(command.Id, ct: cancellationToken);
    }
}
