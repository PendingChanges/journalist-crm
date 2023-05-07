using System;
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
    public class CreatePitchHandlerShould
    {
        private readonly Mock<IStoreAggregates> _aggregateStoreMock;

        public CreatePitchHandlerShould()
        {
            _aggregateStoreMock = new Mock<IStoreAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_create_Pitch_properly()
        {
            //Arrange
            var ownerId = new OwnerId("ownerId");
            var handler = new CreatePitchHandler(_aggregateStoreMock.Object);
            var pitchContent = new PitchContent("name", "content");
            var command = new CreatePitch(pitchContent, DateTime.Now, DateTime.Now, "client id", "idea id");
            var wrappedCommand = new WrappedCommand<CreatePitch, Pitch>(command, ownerId);

            //Act
            await handler.Handle(wrappedCommand, CancellationToken.None);

            //Assert
            Assert.True(true);
        }
    }
}
