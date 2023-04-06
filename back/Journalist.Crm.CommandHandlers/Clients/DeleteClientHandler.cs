using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using Journalist.Crm.Marten;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class DeleteClientHandler : IRequestHandler<WrappedCommand<DeleteClient, ClientAggregate>, ClientAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public DeleteClientHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<ClientAggregate> Handle(WrappedCommand<DeleteClient, ClientAggregate> request, CancellationToken cancellationToken)
        {
            var command = request.Command;
            var clientAggregate = await _aggregateStore.LoadAsync<ClientAggregate>(command.Id, ct: cancellationToken);

            if (clientAggregate == null)
            {
                throw new DomainException(new[] { new Domain.Error("AGGREGATE_NOT_FOUND", "Aggregate does not exists") });
            }

            clientAggregate.Delete(request.OwnerId);
            var errors = clientAggregate.GetUncommitedErrors();

            if (errors.Any())
            {
                throw new DomainException(errors);
            }

            await _aggregateStore.StoreAsync(clientAggregate, cancellationToken);
            return clientAggregate;
        }
    }
}
