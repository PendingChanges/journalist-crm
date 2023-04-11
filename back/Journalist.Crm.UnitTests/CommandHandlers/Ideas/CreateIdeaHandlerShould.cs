using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Clients;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Ideas
{
    public class CreateIdeaHandlerShould
    {
        private Mock<IStoreAggregates> _aggregateStoreMock;

        public CreateIdeaHandlerShould()
        {
            _aggregateStoreMock = new Mock<IStoreAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_create_idea_properly()
        {
            //Arrange
            var ownerId = "ownerId";
            var handler = new CreateIdeaHandler(_aggregateStoreMock.Object);
            var command = new CreateIdea("name", "description");
            var wrappedCommand = new WrappedCommand<CreateIdea, IdeaAggregate>(command, ownerId);

            //Act
            await handler.Handle(wrappedCommand, CancellationToken.None);

            //Assert
            Assert.True(true);
        }
    }
}
