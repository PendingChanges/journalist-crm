using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Common;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;

namespace Journalist.Crm.CommandHandlers.Ideas
{
    internal class CreateIdeaHandler : SingleAggregateCommandHandler<CreateIdea, Idea>
    {
        public CreateIdeaHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override AggregateResult ExecuteCommand(Idea aggregate, CreateIdea command, OwnerId ownerId) => AggregateResult.Create();

        protected override Task<Idea?> LoadAggregate(CreateIdea command, OwnerId ownerId,
            CancellationToken cancellationToken)
        {
            //TODO : still a problem here, public constructor + what if Create fails?
            var idea = new Idea();
            idea.Create(command.Name, command.Description, ownerId);
            return Task.FromResult<Idea?>(idea);
        }
    }
}
