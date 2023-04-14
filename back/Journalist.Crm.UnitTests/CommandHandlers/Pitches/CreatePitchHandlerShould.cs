using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Pitches;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using Journalist.Crm.Domain.Pitches.ValueObjects;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Picthes
{
    public class CreatePicthHandlerShould
    {
        private Mock<IStoreAggregates> _aggregateStoreMock;

        public CreatePicthHandlerShould()
        {
            _aggregateStoreMock = new Mock<IStoreAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_create_Picth_properly()
        {
            //Arrange
            var ownerId = "ownerId";
            var handler = new CreatePitchHandler(_aggregateStoreMock.Object);
            var pitchContent = new PitchContent("name", "content");
            var command = new CreatePitch(pitchContent, DateTime.Now, DateTime.Now, "client id", "idea id");
            var wrappedCommand = new WrappedCommand<CreatePitch, PitchAggregate>(command, ownerId);

            //Act
            await handler.Handle(wrappedCommand, CancellationToken.None);

            //Assert
            Assert.True(true);
        }
    }
}
