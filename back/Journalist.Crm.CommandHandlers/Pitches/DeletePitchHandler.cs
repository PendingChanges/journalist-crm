using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using Journalist.Crm.Marten;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Pitchs
{
    internal class DeletePitchHandler : IRequestHandler<DeletePitch, PitchAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public DeletePitchHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<PitchAggregate> Handle(DeletePitch request, CancellationToken cancellationToken)
        {
            var pitchAggregate = await _aggregateStore.LoadAsync<PitchAggregate>(request.Id, ct: cancellationToken);

            if (pitchAggregate == null)
            {
                throw new DomainException(new[] { new Domain.Error("AGGREGATE_NOT_FOUND", "Aggregate does not exists") });
            }

            pitchAggregate.Delete(request.Id, request.OwnerId);
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
