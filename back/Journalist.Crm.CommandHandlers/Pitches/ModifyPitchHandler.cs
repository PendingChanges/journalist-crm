using Journalist.Crm.Domain.Pitches.Commands;
using Journalist.Crm.Domain.Pitches;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.ValueObjects;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class ModifyPitchHandler : SingleAggregateCommandHandler<ModifyPitch, Pitch>
    {
        public ModifyPitchHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : base(eventWriter, aggregateReader)
        {
        }

        protected override Task<Pitch?> LoadAggregate(ModifyPitch command, OwnerId ownerId, CancellationToken cancellationToken)
            => AggregateReader.LoadAsync<Pitch>(command.Id, ct: cancellationToken);

        protected override AggregateResult ExecuteCommand(Pitch aggregate, ModifyPitch command, OwnerId ownerId)
            => aggregate.Modify(command.Content, command.DeadLineDate, command.IssueDate, command.ClientId, command.IdeaId, ownerId);
    }
}
