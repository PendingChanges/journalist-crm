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
        private ClientAggregate? _clientAgggregate = null;

        [Given(@"No existing client")]
        public void GivenNoExistingClient()
        {
           // Nothing to do more
        }

        [When(@"A user with id ""([^""]*)"" create a client with name ""([^""]*)""")]
        public void WhenAUserWithIdCreateAClientWithName(string ownerId, string name)
        {
            _clientAgggregate = new ClientAggregate(name, ownerId);
        }

        [Then(@"A client ""([^""]*)"" owned by ""([^""]*)"" is created")]
        public void ThenAClientOwnedByIsCreated(string name, string ownerId)
        {
            Assert.NotNull(_clientAgggregate);
            Assert.Equal(name, _clientAgggregate.Name);
            Assert.Equal(ownerId, _clientAgggregate.OwnerId);

            var @event = _clientAgggregate.GetUncommitedEvents().LastOrDefault() as ClientCreated;

            Assert.NotNull(@event);
            Assert.Equal(name, @event.Name);
            Assert.Equal(ownerId, @event.OwnerId);
            Assert.Equal(_clientAgggregate.Id, @event.Id);
        }

        [Given(@"An existing client with name ""([^""]*)"" and an owner ""([^""]*)""")]
        public void GivenAnExistingClientWithNameAndAnOwner(string name, string ownerId)
        {
            _clientAgggregate = new ClientAggregate(name, ownerId);
        }

        [When(@"A user with id ""([^""]*)"" delete the client")]
        public void WhenAUserWithIdDeleteTheClient(string ownerId)
        {
            Assert.NotNull(_clientAgggregate);

            _clientAgggregate.Delete(_clientAgggregate.Id, ownerId);
        }

        [Then(@"The client is deleted")]
        public void ThenTheClientIsDeleted()
        {
            Assert.NotNull(_clientAgggregate);
            Assert.True(_clientAgggregate.Deleted);

            var @event = _clientAgggregate.GetUncommitedEvents().LastOrDefault() as ClientDeleted;

            Assert.NotNull(@event);
            Assert.Equal(_clientAgggregate.Id, @event.Id);
        }


        [Then(@"An error with code ""([^""]*)"" is raised")]
        public void ThenAnErrorWithCodeIsRaised(string errorCode)
        {
            Assert.NotNull(_clientAgggregate);
            var error = _clientAgggregate.GetUncommitedErrors().FirstOrDefault(e => e.Code == errorCode); 

            Assert.NotNull(error);
        }

        [Then(@"The client is not deleted")]
        public void ThenTheClientIsNotDeleted()
        {
            Assert.NotNull(_clientAgggregate);
            Assert.False(_clientAgggregate.Deleted);

            Assert.DoesNotContain(_clientAgggregate.GetUncommitedEvents(), e => e is ClientDeleted);
        }
    }
}
