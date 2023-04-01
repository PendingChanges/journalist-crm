using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Events;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Events;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace Journalist.Crm.UnitTests.Domain.Ideas
{
    [Binding]
    public class IdeaStepDefinitions
    {

        private readonly AggregateContext _aggregateContext;
        public IdeaStepDefinitions(AggregateContext aggregateContext)
        {
            _aggregateContext = aggregateContext;
        }

        [Given(@"No existing idea")]
        public void GivenNoExistingIdea()
        {
            // Nothing to do more
        }

        [When(@"A user with id ""([^""]*)"" create an idea with name ""([^""]*)"" and descrition ""([^""]*)""")]
        public void WhenAUserWithIdCreateAnIdeaWithNameAndDescrition(string ownerId, string name, string description)
        {
            _aggregateContext.Aggregate = new IdeaAggregate(name, description, ownerId);
        }

        [Then(@"An idea ""([^""]*)"" with description ""([^""]*)"" owned by ""([^""]*)"" is created")]
        public void ThenAnIdeaWithDescriptionOwnedByIsCreated(string name, string description, string ownerId)
        {
            var ideaAggregate = _aggregateContext.Aggregate as IdeaAggregate;
            Assert.NotNull(ideaAggregate);
            Assert.Equal(name, ideaAggregate.Name);
            Assert.Equal(ownerId, ideaAggregate.OwnerId);
            Assert.Equal(description, ideaAggregate.Description);

            var @event = ideaAggregate.GetUncommitedEvents().LastOrDefault() as IdeaCreated;

            Assert.NotNull(@event);
            Assert.Equal(name, @event.Name);
            Assert.Equal(ownerId, @event.OwnerId);
            Assert.Equal(description, @event.Description);
            Assert.Equal(ideaAggregate.Id, @event.Id);
        }

        [Given(@"An existing idea with name ""([^""]*)"", description ""([^""]*)"" and an owner ""([^""]*)""")]
        public void GivenAnExistingIdeaWithNameDescriptionAndAnOwner(string name, string description, string ownerId)
        {
            _aggregateContext.Aggregate = new IdeaAggregate(name, description, ownerId);
        }

        [When(@"A user with id ""([^""]*)"" delete the idea")]
        public void WhenAUserWithIdDeleteTheIdea(string ownerId)
        {
            var ideaAggregate = _aggregateContext.Aggregate as IdeaAggregate;

            Assert.NotNull(ideaAggregate);

            ideaAggregate.Delete(ideaAggregate.Id, ownerId);
        }

        [Then(@"The idea is deleted")]
        public void ThenTheIdeaIsDeleted()
        {
            var ideaAggregate = _aggregateContext.Aggregate as IdeaAggregate;

            Assert.NotNull(ideaAggregate);
            Assert.True(ideaAggregate.Deleted);

            var @event = ideaAggregate.GetUncommitedEvents().LastOrDefault() as IdeaDeleted;

            Assert.NotNull(@event);
            Assert.Equal(ideaAggregate.Id, @event.Id);
        }


        [Then(@"The idea is not deleted")]
        public void ThenTheIdeaIsNotDeleted()
        {
            var ideaAggregate = _aggregateContext.Aggregate as IdeaAggregate;

            Assert.NotNull(ideaAggregate);
            Assert.False(ideaAggregate.Deleted);

            Assert.DoesNotContain(ideaAggregate.GetUncommitedEvents(), e => e is IdeaDeleted);
        }

    }
}
