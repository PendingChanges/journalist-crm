﻿using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using Journalist.Crm.Marten;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class CreateClientHandler : IRequestHandler<CreateClient, ClientAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public CreateClientHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<ClientAggregate> Handle(CreateClient request, CancellationToken cancellationToken)
        {
            var clientAggregate = new ClientAggregate(request.Name, request.OwnerId);

            //Store Aggregate
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
