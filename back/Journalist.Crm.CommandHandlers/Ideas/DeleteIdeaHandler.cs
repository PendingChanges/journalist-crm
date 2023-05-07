using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Common;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;

namespace Journalist.Crm.CommandHandlers.Ideas
{
    internal class DeleteIdeaHandler : SingleAggregateCommandHandler<DeleteIdea, Idea>
    {
        public DeleteIdeaHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override AggregateResult ExecuteCommand(Idea aggregate, DeleteIdea command, OwnerId ownerId) => aggregate.Delete(ownerId);

        protected override Task<Idea?> LoadAggregate(DeleteIdea command, OwnerId ownerId, CancellationToken cancellationToken) => AggregateStore.LoadAsync<Idea>(command.Id, ct: cancellationToken);
    }
}
