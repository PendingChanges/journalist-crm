using Journalist.Crm.CommandHandlers;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Journalist.Crm.GraphQL.Ideas;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.Common;
using Xunit;

namespace Journalist.Crm.UnitTests.GraphQL.Ideas
{
    public class IdeasMutationsShould
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IContext> _contextMock;

        public IdeasMutationsShould()
        {
            _mediatorMock = new Mock<IMediator>();
            _contextMock = new Mock<IContext>();
        }

        [Fact]
        public async Task Dispatch_wrapped_modify_idea_and_return_aggregate_id()
        {
            //Arrange
            var ideasMutations = new IdeasMutations();
            var ownerId = new OwnerId("user id");
            var aggregate = new Idea("name", "description", ownerId);
            var command = new ModifyIdea(aggregate.Id, "new name", "new description");
            _contextMock.Setup(_ => _.UserId).Returns(ownerId);
            _mediatorMock.Setup(_ => _.Send(It.IsAny<WrappedCommand<ModifyIdea, Idea>>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate).Verifiable();

            //Act
            var result = await ideasMutations.ModifyIdeaAsync(_mediatorMock.Object, _contextMock.Object, command, CancellationToken.None);

            //Assert
            Assert.Equal(command.Id, result);
            _mediatorMock.VerifyAll();
        }
    }
}
