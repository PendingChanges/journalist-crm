using Journalist.Crm.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.CommandHandlers
{
    internal abstract class SingleAggregateCommandHandlerBase<TCommand, TAggregate> : IRequestHandler<WrappedCommand<TCommand, TAggregate>, TAggregate>
        where TAggregate : AggregateBase
        where TCommand : ICommand
    {
        protected readonly IStoreAggregates _aggregateStore;

        protected SingleAggregateCommandHandlerBase(IStoreAggregates aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<TAggregate> Handle(WrappedCommand<TCommand, TAggregate> request, CancellationToken cancellationToken)
        {
            var command = request.Command;

            var aggregate = await LoadAggregate(command, cancellationToken);

            if (aggregate == null)
            {
                throw new DomainException(new[] { ErrorBuilder.AggregateNotFound() });
            }

            ExecuteCommand(aggregate, command, request.OwnerId);

            var errors = aggregate.GetUncommitedErrors();
            if (errors.Any())
            {
                throw new DomainException(errors);
            }

            await _aggregateStore.StoreAsync(aggregate, cancellationToken);

            return aggregate;
        }

        protected abstract Task<TAggregate?> LoadAggregate(TCommand command, CancellationToken cancellationToken);

        protected abstract void ExecuteCommand(TAggregate aggregate, TCommand command, string ownerId);
    }
}
