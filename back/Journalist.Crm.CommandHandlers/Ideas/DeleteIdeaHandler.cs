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
    internal class DeleteIdeaHandler : IRequestHandler<DeleteIdea, IdeaAggregate>
    {
        private readonly IStoreAggregates _aggregateStore;

        public DeleteIdeaHandler(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<IdeaAggregate> Handle(DeleteIdea request, CancellationToken cancellationToken)
        {
            var ideaAggregate = await _aggregateStore.LoadAsync<IdeaAggregate>(request.Id, ct: cancellationToken);

            if (ideaAggregate == null)
            {
                throw new DomainException(new[] { new Domain.Error("AGGREGATE_NOT_FOUND", "Aggregate does not exists") });
            }


            ideaAggregate.Delete(request.Id, request.OwnerId);
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
