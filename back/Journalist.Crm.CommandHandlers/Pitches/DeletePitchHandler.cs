using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Pitchs
{
    internal class DeletePitchHandler : IRequestHandler<WrappedCommand<DeletePitch, PitchAggregate>, PitchAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public DeletePitchHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<PitchAggregate> Handle(WrappedCommand<DeletePitch, PitchAggregate> request, CancellationToken cancellationToken)
        {
            var command = request.Command;

            var pitchAggregate = await _aggregateStore.LoadAsync<PitchAggregate>(command.Id, ct: cancellationToken);

            if (pitchAggregate == null)
            {
                throw new DomainException(new[] { new Domain.Error("AGGREGATE_NOT_FOUND", "Aggregate does not exists") });
            }

            pitchAggregate.Delete(command.Id, request.OwnerId);
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
