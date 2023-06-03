using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.CommandHandlers.Ideas
{
    internal class CreateIdeaHandler : SingleAggregateCommandHandler<CreateIdea, Idea>
    {
        public CreateIdeaHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : base(eventWriter, aggregateReader) { }

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
