using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.CommandHandlers.Ideas
{
    internal class ModifyIdeaHandler : SingleAggregateCommandHandler<ModifyIdea, Idea>
    {
        public ModifyIdeaHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : base(eventWriter, aggregateReader) { }

        protected override AggregateResult ExecuteCommand(Idea aggregate, ModifyIdea command, OwnerId ownerId) => aggregate.Modify(command.NewName, command.NewDescription, ownerId);

        protected override Task<Idea?> LoadAggregate(ModifyIdea command, OwnerId ownerId, CancellationToken cancellationToken) => AggregateReader.LoadAsync<Idea>(command.Id, ct: cancellationToken);
    }
}
