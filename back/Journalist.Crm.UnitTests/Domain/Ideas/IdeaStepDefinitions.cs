using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Events;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;
using Journalist.Crm.Domain.Common;

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
        public void WhenAUserWithIdCreateAnIdeaWithNameAndDescription(string ownerId, string name, string description)
        {
            var aggregate = new Idea();
            _aggregateContext.Result = aggregate.Create(name, description, new OwnerId(ownerId));
            _aggregateContext.Aggregate = aggregate;
        }

        [Then(@"An idea ""([^""]*)"" with description ""([^""]*)"" owned by ""([^""]*)"" is created")]
        public void ThenAnIdeaWithDescriptionOwnedByIsCreated(string name, string description, string ownerId)
        {
            var ideaAggregate = _aggregateContext.Aggregate as Idea;
            Assert.NotNull(ideaAggregate);
            Assert.Equal(name, ideaAggregate.Name);
            Assert.Equal(ownerId, ideaAggregate.OwnerId);
            Assert.Equal(description, ideaAggregate.Description);

            var @event = _aggregateContext.GetEvents().LastOrDefault() as IdeaCreated;

            Assert.NotNull(@event);
            Assert.Equal(name, @event.Name);
            Assert.Equal(ownerId, @event.OwnerId);
            Assert.Equal(description, @event.Description);
            Assert.Equal(ideaAggregate.Id, @event.Id);
        }

        [Given(@"An existing idea with name ""([^""]*)"", description ""([^""]*)"" and an owner ""([^""]*)""")]
        public void GivenAnExistingIdeaWithNameDescriptionAndAnOwner(string name, string description, string ownerId)
        {
            var aggregate = new Idea();
            aggregate.Create(name, description, new OwnerId(ownerId));
            _aggregateContext.Aggregate = aggregate;
        }

        [When(@"A user with id ""([^""]*)"" delete the idea")]
        public void WhenAUserWithIdDeleteTheIdea(string ownerId)
        {
            var ideaAggregate = _aggregateContext.Aggregate as Idea;

            Assert.NotNull(ideaAggregate);

            _aggregateContext.Result = ideaAggregate.Delete(new OwnerId(ownerId));
        }

        [Then(@"The idea is deleted")]
        public void ThenTheIdeaIsDeleted()
        {
            var ideaAggregate = _aggregateContext.Aggregate as Idea;

            Assert.NotNull(ideaAggregate);
            Assert.True(ideaAggregate.Deleted);

            var @event = _aggregateContext.GetEvents().LastOrDefault() as IdeaDeleted;

            Assert.NotNull(@event);
            Assert.Equal(ideaAggregate.Id, @event.Id);
        }


        [Then(@"The idea is not deleted")]
        public void ThenTheIdeaIsNotDeleted()
        {
            var ideaAggregate = _aggregateContext.Aggregate as Idea;

            Assert.NotNull(ideaAggregate);
            Assert.False(ideaAggregate.Deleted);

            Assert.DoesNotContain(_aggregateContext.GetEvents(), e => e is IdeaDeleted);
        }

        [When(@"A user with id ""([^""]*)"" modify the idea to new name ""([^""]*)"" and new description ""([^""]*)""")]
        public void WhenAUserWithIdModifyTheIdeaToNewNameAndNewDescription(string ownerId, string newName, string newDescription)
        {
            var ideaAggregate = _aggregateContext.Aggregate as Idea;

            _aggregateContext.Result = ideaAggregate?.Modify(newName, newDescription, new OwnerId(ownerId));
        }

        [Then(@"The idea is modified with new name ""([^""]*)"" and new description ""([^""]*)""")]
        public void ThenTheIdeaIsModifiedWithNewNameAndNewDescription(string newName, string newDescription)
        {
            var ideaAggregate = _aggregateContext.Aggregate as Idea;

            Assert.NotNull(ideaAggregate);
            Assert.Equal(newName, ideaAggregate.Name);
            Assert.Equal(newDescription, ideaAggregate.Description);

            var events = _aggregateContext.GetEvents().ToList();
            Assert.Single(events);
            var @event = events.LastOrDefault() as IdeaModified;

            Assert.NotNull(@event);
            Assert.Equal(ideaAggregate.Id, @event.Id);
            Assert.Equal(newName, @event.NewName);
            Assert.Equal(newDescription, @event.NewDescription);
        }
    }
}
