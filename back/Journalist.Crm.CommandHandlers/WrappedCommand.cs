using Journalist.Crm.Domain;
using MediatR;

namespace Journalist.Crm.CommandHandlers
{
    public class WrappedCommand<TCommand, TAggregate> : IRequest<TAggregate>
        where TCommand : ICommand
        where TAggregate : AggregateBase
    {
        public WrappedCommand(TCommand command, string ownerId)
        {
            Command = command;
            OwnerId = ownerId;

        }
        public TCommand Command { get; }

        public string OwnerId { get; }
    }
}
