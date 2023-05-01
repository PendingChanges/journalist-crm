using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Common;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;

namespace Journalist.Crm.CommandHandlers.Ideas
{
    internal class CreateIdeaHandler : SingleAggregateCommandHandlerBase<CreateIdea, Idea>
    {
        public CreateIdeaHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(Idea aggregate, CreateIdea command, OwnerId ownerId)
        {
        }

        protected override Task<Idea?> LoadAggregate(CreateIdea command, OwnerId ownerId, CancellationToken cancellationToken)
            => Task.FromResult<Idea?>(new Idea(command.Name, command.Description, ownerId));
    }
}
