using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class CreateIdeaHandler : SingleAggregateCommandHandlerBase<CreateIdea, IdeaAggregate>
    {
        public CreateIdeaHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(IdeaAggregate aggregate, CreateIdea command, string ownerId)
        {
        }

        protected override Task<IdeaAggregate?> LoadAggregate(CreateIdea command, string ownerId, CancellationToken cancellationToken)
            => Task.FromResult<IdeaAggregate?>(new IdeaAggregate(command.Name, command.Description, ownerId));
    }
}
