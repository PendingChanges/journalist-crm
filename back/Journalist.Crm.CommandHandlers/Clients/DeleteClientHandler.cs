using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using Journalist.Crm.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class DeleteClientHandler : SingleAggregateCommandHandler<DeleteClient, Client>
    {
        public DeleteClientHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(Client aggregate, DeleteClient command, OwnerId ownerId)
            => aggregate.Delete(ownerId);

        protected override Task<Client?> LoadAggregate(DeleteClient command, OwnerId ownerId, CancellationToken cancellationToken)
            => AggregateStore.LoadAsync<Client>(command.Id, ct: cancellationToken);
    }
}
