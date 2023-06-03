using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using Journalist.Crm.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class DeletePitchHandler : SingleAggregateCommandHandler<DeletePitch, Pitch>
    {
        public DeletePitchHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : base(eventWriter, aggregateReader) { }

        protected override AggregateResult ExecuteCommand(Pitch aggregate, DeletePitch command, OwnerId ownerId)
            => aggregate.Cancel(ownerId);

        protected override Task<Pitch?> LoadAggregate(DeletePitch command, OwnerId ownerId, CancellationToken cancellationToken)
            => AggregateReader.LoadAsync<Pitch>(command.Id, ct: cancellationToken);
    }
}
