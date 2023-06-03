using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class DeleteClientHandler : SingleAggregateCommandHandler<DeleteClient, Client>
    {
        public DeleteClientHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : base(eventWriter, aggregateReader) { }

        protected override AggregateResult ExecuteCommand(Client aggregate, DeleteClient command, OwnerId ownerId)
            => aggregate.Delete(ownerId);

        protected override Task<Client?> LoadAggregate(DeleteClient command, OwnerId ownerId, CancellationToken cancellationToken)
            => AggregateReader.LoadAsync<Client>(command.Id, ct: cancellationToken);
    }
}
