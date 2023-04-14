using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class CreatePitchHandler : SingleAggregateCommandHandlerBase<CreatePitch, PitchAggregate>
    {
        public CreatePitchHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(PitchAggregate aggregate, CreatePitch command, string ownerId)
        {
        }

        protected override Task<PitchAggregate?> LoadAggregate(CreatePitch command, string ownerId, CancellationToken cancellationToken)
         => Task.FromResult<PitchAggregate?>(new PitchAggregate(command.Content, command.DeadLineDate, command.IssueDate, command.ClientId, command.IdeaId, ownerId));
    }
}
