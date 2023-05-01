using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class RenameClientHandler : SingleAggregateCommandHandlerBase<RenameClient, Client>
    {
        public RenameClientHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(Client aggregate, RenameClient command, OwnerId ownerId)
            => aggregate.Rename(command.NewName, ownerId);


        protected override Task<Client?> LoadAggregate(RenameClient command, OwnerId ownerId, CancellationToken cancellationToken)
            => _aggregateStore.LoadAsync<Client>(command.Id, ct: cancellationToken);
    }
}
