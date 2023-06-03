using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.ValueObjects;
using MediatR;

namespace Journalist.Crm.CommandHandlers
{
    public class WrappedCommand<TCommand, TAggregate> : IRequest<TAggregate>
        where TCommand : ICommand
        where TAggregate : Aggregate
    {
        public WrappedCommand(TCommand command, OwnerId ownerId)
        {
            Command = command;
            OwnerId = ownerId;

        }
        public TCommand Command { get; }

        public OwnerId OwnerId { get; }
    }
}
