using Journalist.Crm.Domain.Clients.Events;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Events;
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
            var aggregate = new IdeaAggregate();
            aggregate.Create(name, description, ownerId);
            _aggregateContext.Aggregate = aggregate;
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
            var aggregate = new IdeaAggregate();
            aggregate.Create(name, description, ownerId);
            aggregate.ClearUncommitedEvents();
            _aggregateContext.Aggregate = aggregate;
        }

        [When(@"A user with id ""([^""]*)"" delete the idea")]
        public void WhenAUserWithIdDeleteTheIdea(string ownerId)
        {
            var ideaAggregate = _aggregateContext.Aggregate as IdeaAggregate;

            Assert.NotNull(ideaAggregate);

            ideaAggregate.Delete( ownerId);
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

        [When(@"A user with id ""([^""]*)"" modify the idea to new name ""([^""]*)"" and new description ""([^""]*)""")]
        public void WhenAUserWithIdModifyTheIdeaToNewNameAndNewDescription(string ownerId, string newName, string newDescrition)
        {
            var ideaAggregate = _aggregateContext.Aggregate as IdeaAggregate;

            if(ideaAggregate == null)
            {
                return;
            }

            ideaAggregate.Modify(newName, newDescrition, ownerId);
        }

        [Then(@"The idea is modified with new name ""([^""]*)"" and new description ""([^""]*)""")]
        public void ThenTheIdeaIsModifiedWithNewNameAndNewDescription(string newName, string newDescription)
        {
            var ideaAggregate = _aggregateContext.Aggregate as IdeaAggregate;

            Assert.NotNull(ideaAggregate);
            Assert.Equal(newName, ideaAggregate.Name);
            Assert.Equal(newDescription, ideaAggregate.Description);

            var events = ideaAggregate.GetUncommitedEvents().ToList();
            Assert.Single(events);
            var @event = events.LastOrDefault() as IdeaModified;

            Assert.NotNull(@event);
            Assert.Equal(ideaAggregate.Id, @event.Id);
            Assert.Equal(newName, @event.NewName);
            Assert.Equal(newDescription, @event.NewDescription);
        }
    }
}
