﻿using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using Journalist.Crm.Marten;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class CreatePitchHandler : IRequestHandler<CreatePitch, PitchAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public CreatePitchHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<PitchAggregate> Handle(CreatePitch request, CancellationToken cancellationToken)
        {
            //TODO: Check existence of clientId and ideaId
            var pitchAggregate = new PitchAggregate();

            pitchAggregate.Create(request.Title, request.Content, request.DeadLineDate, request.IssueDate, request.ClientId, request.IdeaId, request.OwnerId);

            //Store Aggregate
            var errors = pitchAggregate.GetUncommitedErrors();
            if (errors.Any())
            {
                throw new DomainException(errors);
            }

            await _aggregateStore.StoreAsync(pitchAggregate, cancellationToken);
            return pitchAggregate;
        }
    }
}
