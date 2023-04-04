using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Journalist.Crm.Marten;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers.Clients
{
    internal class CreateIdeaHandler : IRequestHandler<CreateIdea, IdeaAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public CreateIdeaHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<IdeaAggregate> Handle(CreateIdea request, CancellationToken cancellationToken)
        {
            var ideaAggregate = new IdeaAggregate();

            ideaAggregate.Create(request.Name, request.Description, request.OwnerId);

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
