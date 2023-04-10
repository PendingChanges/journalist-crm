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
    internal class CreateIdeaHandler : IRequestHandler<WrappedCommand<CreateIdea, IdeaAggregate>, IdeaAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public CreateIdeaHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<IdeaAggregate> Handle(WrappedCommand<CreateIdea, IdeaAggregate> request, CancellationToken cancellationToken)
        {
            var ideaAggregate = new IdeaAggregate();

            var command = request.Command;
            ideaAggregate.Create(command.Name, command.Description, request.OwnerId);

            //Store Aggregate
            var errors = ideaAggregate.GetUncommitedErrors();
            if (errors.Any())
            {
                throw new DomainException(errors);
            }

            await _aggregateStore.StoreAsync(ideaAggregate, cancellationToken);

            return ideaAggregate;
        }
    }
}
