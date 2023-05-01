using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class CreatePitchHandler : SingleAggregateCommandHandlerBase<CreatePitch, Pitch>
    {
        public CreatePitchHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(Pitch aggregate, CreatePitch command, OwnerId ownerId)
        {
        }

        protected override Task<Pitch?> LoadAggregate(CreatePitch command, OwnerId ownerId, CancellationToken cancellationToken)
         => Task.FromResult<Pitch?>(new Pitch(command.Content, command.DeadLineDate, command.IssueDate, command.ClientId, command.IdeaId, ownerId));
    }
}
