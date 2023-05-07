using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Pitches;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Common;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using Moq;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Pitches
{
    public class DeletePitchHandlerShould
    {
        private readonly Mock<IStoreAggregates> _aggregateStoreMock;

        public DeletePitchHandlerShould()
        {
            _aggregateStoreMock = new Mock<IStoreAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_delete_Pitch_properly()
        {
            //Arrange
            var ownerId = new OwnerId("ownerId");
            var pitchContent = new PitchContent("name", "content");
            var aggregate = new Pitch();
            aggregate.Create(pitchContent, DateTime.Now, DateTime.Now, "client id", "idea id", ownerId);
            _aggregateStoreMock.Setup(_ => _.LoadAsync<Pitch>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
            var handler = new DeletePitchHandler(_aggregateStoreMock.Object);
            var command = new DeletePitch(aggregate.Id);
            var wrappedCommand = new WrappedCommand<DeletePitch, Pitch>(command, ownerId);

            //Act
            var aggregateInReturn = await handler.Handle(wrappedCommand, CancellationToken.None);

            //Assert
            _aggregateStoreMock.Verify(_ => _.StoreAsync(aggregateInReturn.Id, aggregateInReturn.Version, It.IsAny<IEnumerable<object>>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Throw_domain_exception_when_aggregate_does_not_exists()
        {
            //Arrange
            var ownerId = new OwnerId("ownerId");
            var aggregateId = EntityId.NewEntityId();
            _aggregateStoreMock.Setup(_ => _.LoadAsync<Pitch>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((Pitch?)null);
            var handler = new DeletePitchHandler(_aggregateStoreMock.Object);
            var command = new DeletePitch(aggregateId);
            var wrappedCommand = new WrappedCommand<DeletePitch, Pitch>(command, ownerId);

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
