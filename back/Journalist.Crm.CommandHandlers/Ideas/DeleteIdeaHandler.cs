using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class DeleteIdeaHandler : SingleAggregateCommandHandlerBase<DeleteIdea, IdeaAggregate>
    {
        public DeleteIdeaHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(IdeaAggregate aggregate, DeleteIdea command, string ownerId) => aggregate.Delete(ownerId);

        protected override Task<IdeaAggregate?> LoadAggregate(DeleteIdea command, string ownerId, CancellationToken cancellationToken) => _aggregateStore.LoadAsync<IdeaAggregate>(command.Id, ct: cancellationToken);
    }
}
