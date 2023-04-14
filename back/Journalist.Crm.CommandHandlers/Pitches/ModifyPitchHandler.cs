using Journalist.Crm.Domain.Pitches.Commands;
using Journalist.Crm.Domain.Pitches;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class ModifyPitchHandler : SingleAggregateCommandHandlerBase<ModifyPitch, PitchAggregate>
    {
        public ModifyPitchHandler(IStoreAggregates aggregateStore) : base(aggregateStore)
        {
        }

        protected override Task<PitchAggregate?> LoadAggregate(ModifyPitch command, string ownerId, CancellationToken cancellationToken)
            => _aggregateStore.LoadAsync<PitchAggregate>(command.Id, ct: cancellationToken);

        protected override void ExecuteCommand(PitchAggregate aggregate, ModifyPitch command, string ownerId)
            => aggregate.Modify(command.Content, command.DeadLineDate, command.IssueDate, command.ClientId, command.IdeaId, ownerId);
    }
}
