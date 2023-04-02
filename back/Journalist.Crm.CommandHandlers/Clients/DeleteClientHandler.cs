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
    internal class DeleteClientHandler : IRequestHandler<DeleteClient, ClientAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public DeleteClientHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<ClientAggregate> Handle(DeleteClient request, CancellationToken cancellationToken)
        {
            var clientAggregate = await _aggregateStore.LoadAsync<ClientAggregate>(request.Id, ct: cancellationToken);

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
