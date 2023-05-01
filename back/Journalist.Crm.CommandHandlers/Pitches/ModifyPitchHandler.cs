using Journalist.Crm.Domain.Pitches.Commands;
using Journalist.Crm.Domain.Pitches;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class ModifyPitchHandler : SingleAggregateCommandHandlerBase<ModifyPitch, Pitch>
    {
        public ModifyPitchHandler(IStoreAggregates aggregateStore) : base(aggregateStore)
        {
        }

        protected override Task<Pitch?> LoadAggregate(ModifyPitch command, OwnerId ownerId, CancellationToken cancellationToken)
            => _aggregateStore.LoadAsync<Pitch>(command.Id, ct: cancellationToken);

        protected override void ExecuteCommand(Pitch aggregate, ModifyPitch command, OwnerId ownerId)
            => aggregate.Modify(command.Content, command.DeadLineDate, command.IssueDate, command.ClientId, command.IdeaId, ownerId);
    }
}
