﻿using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using Journalist.Crm.Marten;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class RenameClientHandler : IRequestHandler<RenameClient, ClientAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public RenameClientHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<ClientAggregate> Handle(RenameClient request, CancellationToken cancellationToken)
        {
            var clientAggregate = await _aggregateStore.LoadAsync<ClientAggregate>(request.Id, ct: cancellationToken);

            if (clientAggregate == null)
            {
                throw new DomainException(new[] { new Domain.Error("AGGREGATE_NOT_FOUND", "Aggregate does not exists") });
            }

            clientAggregate.Rename(request.NewName, request.OwnerId);
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
