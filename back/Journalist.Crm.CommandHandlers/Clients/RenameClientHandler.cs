using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class RenameClientHandler : SingleAggregateCommandHandler<RenameClient, Client>
    {
        public RenameClientHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override AggregateResult ExecuteCommand(Client aggregate, RenameClient command, OwnerId ownerId)
            => aggregate.Rename(command.NewName, ownerId);


        protected override Task<Client?> LoadAggregate(RenameClient command, OwnerId ownerId, CancellationToken cancellationToken)
            => AggregateStore.LoadAsync<Client>(command.Id, ct: cancellationToken);
    }
}
