using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Common;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;

namespace Journalist.Crm.CommandHandlers.Ideas
{
    internal class ModifyIdeaHandler : SingleAggregateCommandHandler<ModifyIdea, Idea>
    {
        public ModifyIdeaHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override AggregateResult ExecuteCommand(Idea aggregate, ModifyIdea command, OwnerId ownerId) => aggregate.Modify(command.NewName, command.NewDescription, ownerId);

        protected override Task<Idea?> LoadAggregate(ModifyIdea command, OwnerId ownerId, CancellationToken cancellationToken) => AggregateStore.LoadAsync<Idea>(command.Id, ct: cancellationToken);
    }
}
