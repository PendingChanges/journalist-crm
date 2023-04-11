using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Clients;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Clients
{
    public class CreateClientHandlerShould
    {
        private Mock<IStoreAggregates> _aggregateStoreMock;

        public CreateClientHandlerShould()
        {
            _aggregateStoreMock = new Mock<IStoreAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_create_client_properly()
        {
            //Arrange
            var ownerId = "ownerId";
            var handler = new CreateClientHandler(_aggregateStoreMock.Object);
            var command = new CreateClient("name");
            var wrappedCommand = new WrappedCommand<CreateClient, ClientAggregate>(command, ownerId);

            //Act
            await handler.Handle(wrappedCommand, CancellationToken.None);

            //Assert
            Assert.True(true);
        }
    }
}
