using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Common;
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
