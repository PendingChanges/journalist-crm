using Journalist.Crm.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.CommandHandlers
{
    internal abstract class SingleAggregateCommandHandler<TCommand, TAggregate> : IRequestHandler<WrappedCommand<TCommand, TAggregate>, TAggregate>
        where TAggregate : Aggregate
        where TCommand : ICommand
    {
        protected readonly IStoreAggregates AggregateStore;

        protected SingleAggregateCommandHandler(IStoreAggregates aggregateStore)
        {
            AggregateStore = aggregateStore;
        }

        public async Task<TAggregate> Handle(WrappedCommand<TCommand, TAggregate> request, CancellationToken cancellationToken)
        {
            var command = request.Command;

            var aggregate = await LoadAggregate(command, request.OwnerId, cancellationToken);

            if (aggregate == null)
            {
                throw new DomainException(new[] { ErrorBuilder.AggregateNotFound() });
            }

            ExecuteCommand(aggregate, command, request.OwnerId);

            var errors = aggregate.GetUncommittedErrors().ToList();
            if (errors.Any())
            {
                throw new DomainException(errors);
            }

            await AggregateStore.StoreAsync(aggregate, cancellationToken);

            return aggregate;
        }

        protected abstract Task<TAggregate?> LoadAggregate(TCommand command, OwnerId ownerId, CancellationToken cancellationToken);

        protected abstract void ExecuteCommand(TAggregate aggregate, TCommand command, OwnerId ownerId);
    }
}
