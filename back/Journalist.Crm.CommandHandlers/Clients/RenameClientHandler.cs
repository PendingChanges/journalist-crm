using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using System.Threading.Tasks;
using System.Threading;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class RenameClientHandler : SingleAggregateCommandHandlerBase<RenameClient, ClientAggregate>
    {
        public RenameClientHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(ClientAggregate aggregate, RenameClient command, string ownerId)
            => aggregate.Rename(command.NewName, ownerId);


        protected override Task<ClientAggregate?> LoadAggregate(RenameClient command, string ownerId, CancellationToken cancellationToken)
            => _aggregateStore.LoadAsync<ClientAggregate>(command.Id, ct: cancellationToken);
    }
}
