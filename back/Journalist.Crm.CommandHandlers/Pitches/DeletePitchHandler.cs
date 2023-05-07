using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Common;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class DeletePitchHandler : SingleAggregateCommandHandler<DeletePitch, Pitch>
    {
        public DeletePitchHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(Pitch aggregate, DeletePitch command, OwnerId ownerId)
            => aggregate.Delete(ownerId);

        protected override Task<Pitch?> LoadAggregate(DeletePitch command, OwnerId ownerId, CancellationToken cancellationToken)
            => AggregateStore.LoadAsync<Pitch>(command.Id, ct: cancellationToken);
    }
}
