using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.CommandHandlers.Ideas
{
    internal class DeleteIdeaHandler : SingleAggregateCommandHandler<DeleteIdea, Idea>
    {
        public DeleteIdeaHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : base(eventWriter, aggregateReader) { }

        protected override AggregateResult ExecuteCommand(Idea aggregate, DeleteIdea command, OwnerId ownerId) => aggregate.Delete(ownerId);

        protected override Task<Idea?> LoadAggregate(DeleteIdea command, OwnerId ownerId, CancellationToken cancellationToken) => AggregateReader.LoadAsync<Idea>(command.Id, ct: cancellationToken);
    }
}
