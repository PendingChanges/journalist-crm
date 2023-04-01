using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Events;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace Journalist.Crm.UnitTests.Domain.Clients
{
    [Binding]
    public class ClientStepDefinitions
    {

        private readonly AggregateContext _aggregateContext;
        public ClientStepDefinitions(AggregateContext aggregateContext)
        {
            _aggregateContext = aggregateContext;
        }

        [Given(@"No existing client")]
        public void GivenNoExistingClient()
        {
           // Nothing to do more
        }

        [When(@"A user with id ""([^""]*)"" create a client with name ""([^""]*)""")]
        public void WhenAUserWithIdCreateAClientWithName(string ownerId, string name)
        {
            _aggregateContext.Aggregate = new ClientAggregate(name, ownerId);
        }

        [Then(@"A client ""([^""]*)"" owned by ""([^""]*)"" is created")]
        public void ThenAClientOwnedByIsCreated(string name, string ownerId)
        {
            var clientAggregate = _aggregateContext.Aggregate as ClientAggregate;
            Assert.NotNull(clientAggregate);
            Assert.Equal(name, clientAggregate.Name);
            Assert.Equal(ownerId, clientAggregate.OwnerId);

            var @event = clientAggregate.GetUncommitedEvents().LastOrDefault() as ClientCreated;

            Assert.NotNull(@event);
            Assert.Equal(name, @event.Name);
            Assert.Equal(ownerId, @event.OwnerId);
            Assert.Equal(clientAggregate.Id, @event.Id);
        }

        [Given(@"An existing client with name ""([^""]*)"" and an owner ""([^""]*)""")]
        public void GivenAnExistingClientWithNameAndAnOwner(string name, string ownerId)
        {
            _aggregateContext.Aggregate = new ClientAggregate(name, ownerId);
        }

        [When(@"A user with id ""([^""]*)"" delete the client")]
        public void WhenAUserWithIdDeleteTheClient(string ownerId)
        {
            var clientAggregate = _aggregateContext.Aggregate as ClientAggregate;

            Assert.NotNull(clientAggregate);

            clientAggregate.Delete(clientAggregate.Id, ownerId);
        }

        [Then(@"The client is deleted")]
        public void ThenTheClientIsDeleted()
        {
            var clientAggregate = _aggregateContext.Aggregate as ClientAggregate;

            Assert.NotNull(clientAggregate);
            Assert.True(clientAggregate.Deleted);

            var @event = clientAggregate.GetUncommitedEvents().LastOrDefault() as ClientDeleted;

            Assert.NotNull(@event);
            Assert.Equal(clientAggregate.Id, @event.Id);
        }




        [Then(@"The client is not deleted")]
        public void ThenTheClientIsNotDeleted()
        {
            var clientAggregate = _aggregateContext.Aggregate as ClientAggregate;

            Assert.NotNull(clientAggregate);
            Assert.False(clientAggregate.Deleted);

            Assert.DoesNotContain(clientAggregate.GetUncommitedEvents(), e => e is ClientDeleted);
        }
    }
}
