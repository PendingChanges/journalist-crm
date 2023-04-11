using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class DeletePitchHandler : SingleAggregateCommandHandlerBase<DeletePitch, PitchAggregate>
    {
        public DeletePitchHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(PitchAggregate aggregate, DeletePitch command, string ownerId)
            => aggregate.Delete(ownerId);

        protected override Task<PitchAggregate?> LoadAggregate(DeletePitch command, string ownerId, CancellationToken cancellationToken)
            => _aggregateStore.LoadAsync<PitchAggregate>(command.Id, ct: cancellationToken);
    }
}
