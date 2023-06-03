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
using Journalist.Crm.Domain.ValueObjects;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.UnitTests.CommandHandlers.Clients
{
    public class RenameClientHandlerShould
    {
        private readonly Mock<IWriteEvents> _eventWriterMock;
        private readonly Mock<IReadAggregates> _aggregateReaderMock;

        public RenameClientHandlerShould()
        {
            _eventWriterMock = new Mock<IWriteEvents>();
            _aggregateReaderMock = new Mock<IReadAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_Rename_Client_properly()
        {
            //Arrange
            var ownerId = new OwnerId("user id");
            var aggregate = new Client();
            aggregate.Create("name", ownerId);
            _aggregateReaderMock.Setup(_ => _.LoadAsync<Client>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
            var handler = new RenameClientHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
            var command = new RenameClient(aggregate.Id, "new name");
            var wrappedCommand = new WrappedCommand<RenameClient, Client>(command, ownerId);

            //Act
            var aggregateInReturn = await handler.Handle(wrappedCommand, CancellationToken.None);

            //Assert
            _eventWriterMock.Verify(_ => _.StoreAsync(aggregateInReturn.Id, aggregateInReturn.Version, It.IsAny<IEnumerable<object>>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Throw_domain_exception_when_aggregate_does_not_exists()
        {
            //Arrange
            var ownerId = new OwnerId("user id");
            var aggregateId = EntityId.NewEntityId();
            _aggregateReaderMock.Setup(_ => _.LoadAsync<Client>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((Client?)null);
            var handler = new RenameClientHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
            var command = new RenameClient(aggregateId, "new name");
            var wrappedCommand = new WrappedCommand<RenameClient, Client>(command, ownerId);

            //Act
            var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(wrappedCommand, CancellationToken.None));

            //Assert
            Assert.Single(exception.DomainErrors);
            var domainError = exception.DomainErrors.FirstOrDefault();
            Assert.NotNull(domainError);
            if (domainError != null)
            {
                Assert.Equal(Errors.AGGREGATE_NOT_FOUND.CODE, domainError.Code);
            }
        }
    }
}
