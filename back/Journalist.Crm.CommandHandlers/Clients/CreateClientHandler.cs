using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.ValueObjects;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class CreateClientHandler : SingleAggregateCommandHandler<CreateClient, Client>
    {
        public CreateClientHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : base(eventWriter, aggregateReader) { }

        protected override AggregateResult ExecuteCommand(Client aggregate, CreateClient command, OwnerId ownerId) => AggregateResult.Create();

        protected override Task<Client?> LoadAggregate(CreateClient command, OwnerId ownerId,
            CancellationToken cancellationToken)
        {
            //TODO : still a problem here, public constructor + what if Create fails?
            var client = new Client();
            client.Create(command.Name, ownerId);
            return Task.FromResult<Client?>(client);
        }
    }
}
