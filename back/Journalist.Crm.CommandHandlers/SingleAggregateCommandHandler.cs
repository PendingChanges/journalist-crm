using Journalist.Crm.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.ValueObjects;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.CommandHandlers
{
    internal abstract class SingleAggregateCommandHandler<TCommand, TAggregate> : IRequestHandler<WrappedCommand<TCommand, TAggregate>, TAggregate>
        where TAggregate : Aggregate
        where TCommand : ICommand
    {
        protected readonly IWriteEvents EventWriter;
        protected readonly IReadAggregates AggregateReader;

        protected SingleAggregateCommandHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader)
        {
            EventWriter = eventWriter;
            AggregateReader = aggregateReader;
        }

        public async Task<TAggregate> Handle(WrappedCommand<TCommand, TAggregate> request, CancellationToken cancellationToken)
        {
            var command = request.Command;

            var aggregate = await LoadAggregate(command, request.OwnerId, cancellationToken);

            if (aggregate == null)
            {
                throw new DomainException(new[] { ErrorBuilder.AggregateNotFound() });
            }

            var aggregateResult = ExecuteCommand(aggregate, command, request.OwnerId);

            if (aggregateResult.HasErrors)
            {
                throw new DomainException(aggregateResult.GetErrors());
            }

            await EventWriter.StoreAsync(aggregate.Id, aggregate.Version, aggregateResult.GetEvents(), cancellationToken);

            return aggregate;
        }

        protected abstract Task<TAggregate?> LoadAggregate(TCommand command, OwnerId ownerId, CancellationToken cancellationToken);

        protected abstract AggregateResult ExecuteCommand(TAggregate aggregate, TCommand command, OwnerId ownerId);
    }
}
