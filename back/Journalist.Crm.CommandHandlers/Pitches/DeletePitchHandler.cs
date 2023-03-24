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
    internal class DeletePitchHandler : IRequestHandler<DeletePitch, AggregateResult<PitchAggregate>>
    {
        private readonly IStoreAggregates _aggregateStore;

        public DeletePitchHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<AggregateResult<PitchAggregate>> Handle(DeletePitch request, CancellationToken cancellationToken)
        {
            var pitchAggregate = await _aggregateStore.LoadAsync<PitchAggregate>(request.Id, ct: cancellationToken);

            var result = new AggregateResult<PitchAggregate>(pitchAggregate);

            if (pitchAggregate == null)
            {
                result.AddErrors(new Domain.Error("AGGREGATE_NOT_FOUND", "Aggregate does not exists"));

                return result;
            }

            pitchAggregate.Delete(request.Id, request.OwnerId);
            var errors = pitchAggregate.GetUncommitedErrors();
            if (!errors.Any())
            {
                await _aggregateStore.StoreAsync(pitchAggregate, cancellationToken);
            }

            return result;
        }
    }
}
