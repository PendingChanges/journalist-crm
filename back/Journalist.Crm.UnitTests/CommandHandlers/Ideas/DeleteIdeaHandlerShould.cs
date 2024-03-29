﻿using System.Collections.Generic;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Clients;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.CommandHandlers.Ideas;
using Xunit;
using Journalist.Crm.Domain.Common;
using Journalist.Crm.Domain.ValueObjects;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.UnitTests.CommandHandlers.Ideas
{
    public class DeleteIdeaHandlerShould
    {
        private readonly Mock<IWriteEvents> _eventWriterMock;
        private readonly Mock<IReadAggregates> _aggregateReaderMock;

        public DeleteIdeaHandlerShould()
        {
            _eventWriterMock = new Mock<IWriteEvents>();
            _aggregateReaderMock = new Mock<IReadAggregates>();
        }

        [Fact]
        public async Task Handle_wrapped_command_delete_idea_properly()
        {
            //Arrange
            var ownerId = new OwnerId("user id");
            var aggregate = new Idea();
            aggregate.Create("name", "description", ownerId);
            _aggregateReaderMock.Setup(_ => _.LoadAsync<Idea>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
            var handler = new DeleteIdeaHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
            var command = new DeleteIdea(aggregate.Id);
            var wrappedCommand = new WrappedCommand<DeleteIdea, Idea>(command, ownerId);

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
            _aggregateReaderMock.Setup(_ => _.LoadAsync<Idea>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((Idea?)null);
            var handler = new DeleteIdeaHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
            var command = new DeleteIdea(aggregateId);
            var wrappedCommand = new WrappedCommand<DeleteIdea, Idea>(command, ownerId);

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
