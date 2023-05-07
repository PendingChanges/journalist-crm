using System.Collections.Generic;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Clients;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using Journalist.Crm.Domain.Common;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Clients
{
    public class DeleteClientHandlerShould
    {
        private readonly Mock<IStoreAggregates> _aggregateStoreMock;

        public DeleteClientHandlerShould()
        {
            _aggregateStoreMock = new Mock<IStoreAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_delete_client_properly()
        {
            //Arrange
            var ownerId = new OwnerId("user id");
            var aggregate = new Client();
            aggregate.Create("name", ownerId);
            _aggregateStoreMock.Setup(_ => _.LoadAsync<Client>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
            var handler = new DeleteClientHandler(_aggregateStoreMock.Object);
            var command = new DeleteClient(aggregate.Id);
            var wrappedCommand = new WrappedCommand<DeleteClient, Client>(command, ownerId);

            //Act
            var aggregateInReturn = await handler.Handle(wrappedCommand, CancellationToken.None);

            //Assert
            _aggregateStoreMock.Verify(_ => _.StoreAsync(aggregateInReturn.Id, aggregateInReturn.Version,It.IsAny<IEnumerable<object>>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Throw_domain_exception_when_aggregate_does_not_exists()
        {
            //Arrange
            var ownerId = new OwnerId("user id");
            var aggregateId = EntityId.NewEntityId();
            _aggregateStoreMock.Setup(_ => _.LoadAsync<Client>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((Client?)null);
            var handler = new DeleteClientHandler(_aggregateStoreMock.Object);
            var command = new DeleteClient(aggregateId);
            var wrappedCommand = new WrappedCommand<DeleteClient, Client>(command, ownerId);

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
