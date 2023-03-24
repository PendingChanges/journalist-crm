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
    internal class DeleteClientHandler : IRequestHandler<DeleteClient, AggregateResult<ClientAggregate>>
    {
        private readonly IStoreAggregates _aggregateStore;

        public DeleteClientHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<AggregateResult<ClientAggregate>> Handle(DeleteClient request, CancellationToken cancellationToken)
        {
            var clientAggregate = await _aggregateStore.LoadAsync<ClientAggregate>(request.Id, ct: cancellationToken);

            var result = new AggregateResult<ClientAggregate>(clientAggregate);

            if (clientAggregate == null)
            {
                result.AddErrors(new Domain.Error("AGGREGATE_NOT_FOUND", "Aggregate does not exists"));

                return result;
            }

            clientAggregate.Delete(request.Id, request.OwnerId);
            var errors = clientAggregate.GetUncommitedErrors();
            if (!errors.Any())
            {
                await _aggregateStore.StoreAsync(clientAggregate, cancellationToken);
            }

            return result;
        }
    }
}
