using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Clients;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Ideas
{
    public class DeleteIdeaHandlerShould
    {
        private Mock<IStoreAggregates> _aggregateStoreMock;
 
        public DeleteIdeaHandlerShould()
        {
            _aggregateStoreMock = new Mock<IStoreAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_delete_idea_properly()
        {
            //Arrange
            var ownerId = "ownerId";
            var aggregate = new IdeaAggregate("name", "description", ownerId);
            aggregate.ClearUncommitedEvents();
            _aggregateStoreMock.Setup(_ => _.LoadAsync<IdeaAggregate>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
            var handler = new DeleteIdeaHandler(_aggregateStoreMock.Object);
            var command = new DeleteIdea(aggregate.Id);
            var wrappedCommand = new WrappedCommand<DeleteIdea, IdeaAggregate>(command, ownerId);

            //Act
            var aggregateInReturn = await handler.Handle(wrappedCommand, CancellationToken.None);

            //Assert
            _aggregateStoreMock.Verify(_ => _.StoreAsync(aggregateInReturn, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Throw_domain_exception_when_aggregate_does_not_exists()
        {
            //Arrange
            var ownerId = "ownerId";
            var aggegateId = "id";
            _aggregateStoreMock.Setup(_ => _.LoadAsync<IdeaAggregate>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((IdeaAggregate?)null);
            var handler = new DeleteIdeaHandler(_aggregateStoreMock.Object);
            var command = new DeleteIdea(aggegateId);
            var wrappedCommand = new WrappedCommand<DeleteIdea, IdeaAggregate>(command, ownerId);

            //Act
            var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(wrappedCommand, CancellationToken.None));

            //Assert
            Assert.Single(exception.DomainErrors);
            var domainError = exception.DomainErrors.FirstOrDefault();
            Assert.NotNull(domainError);
            if(domainError != null)
            {
                Assert.Equal(Errors.AGGREGATE_NOT_FOUND.CODE, domainError.Code);
            }
        }
    }
}
