using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Pitches;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Pitchs
{
    public class DeletePitchHandlerShould
    {
        private Mock<IStoreAggregates> _aggregateStoreMock;

        public DeletePitchHandlerShould()
        {
            _aggregateStoreMock = new Mock<IStoreAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_delete_Pitch_properly()
        {
            //Arrange
            var ownerId = "ownerId";
            var aggregate = new PitchAggregate("name", "description", DateTime.Now, DateTime.Now, "client id", "idea id", ownerId);
            aggregate.ClearUncommitedEvents();
            _aggregateStoreMock.Setup(_ => _.LoadAsync<PitchAggregate>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
            var handler = new DeletePitchHandler(_aggregateStoreMock.Object);
            var command = new DeletePitch(aggregate.Id);
            var wrappedCommand = new WrappedCommand<DeletePitch, PitchAggregate>(command, ownerId);

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
            _aggregateStoreMock.Setup(_ => _.LoadAsync<PitchAggregate>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((PitchAggregate?)null);
            var handler = new DeletePitchHandler(_aggregateStoreMock.Object);
            var command = new DeletePitch(aggegateId);
            var wrappedCommand = new WrappedCommand<DeletePitch, PitchAggregate>(command, ownerId);

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
