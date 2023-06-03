using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.ValueObjects;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class RenameClientHandler : SingleAggregateCommandHandler<RenameClient, Client>
    {
        public RenameClientHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : base(eventWriter, aggregateReader) { }

        protected override AggregateResult ExecuteCommand(Client aggregate, RenameClient command, OwnerId ownerId)
            => aggregate.Rename(command.NewName, ownerId);


        protected override Task<Client?> LoadAggregate(RenameClient command, OwnerId ownerId, CancellationToken cancellationToken)
            => AggregateReader.LoadAsync<Client>(command.Id, ct: cancellationToken);
    }
}
