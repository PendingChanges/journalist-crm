using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Events;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace Journalist.Crm.UnitTests.Domain.Pitches
{
    [Binding]
    public class PitchStepDefinitions
    {

        private readonly AggregateContext _aggregateContext;
        public PitchStepDefinitions(AggregateContext aggregateContext)
        {
            _aggregateContext = aggregateContext;
        }

        [Given(@"No existing pitch")]
        public void GivenNoExistingPitch()
        {
            //Nohing to do more
        }

        [When(@"A user with id ""([^""]*)"" create a pitch with title ""([^""]*)"", content ""([^""]*)"", dead line date ""([^""]*)"", issue date ""([^""]*)"", client id ""([^""]*)"" and idea id ""([^""]*)""")]
        public void WhenAUserWithIdCreateAPitchWithTitleContentDeadLineDateIssueDateClientIdAndIdeaId(string ownerId, string title, string content, DateTime? deadLineDate, DateTime? issueDate, string clientId, string ideaId)
        {
            var aggregate = new PitchAggregate(title, content, deadLineDate, issueDate, clientId, ideaId, ownerId);
            _aggregateContext.Aggregate = aggregate;
        }

        [Then(@"A pitch ""([^""]*)"", content ""([^""]*)"", dead line date ""([^""]*)"", issue date ""([^""]*)"", client id ""([^""]*)"" and idea id ""([^""]*)"" owned by ""([^""]*)"" is created")]
        public void ThenAPitchContentDeadLineDateIssueDateClientIdAndIdeaIdOwnedByIsCreated(string title, string content, DateTime? deadLineDate, DateTime? issueDate, string clientId, string ideaId, string ownerId)
        {
            var pitchAggregate = _aggregateContext.Aggregate as PitchAggregate;
            Assert.NotNull(pitchAggregate);
            Assert.Equal(title, pitchAggregate.Title);
            Assert.Equal(content, pitchAggregate.Content);
            Assert.Equal(deadLineDate, pitchAggregate.DeadLineDate);
            Assert.Equal(issueDate, pitchAggregate.IssueDate);
            Assert.Equal(clientId, pitchAggregate.ClientId);
            Assert.Equal(ideaId, pitchAggregate.IdeaId);
            Assert.Equal(ownerId, pitchAggregate.OwnerId);

            var events = pitchAggregate.GetUncommitedEvents().ToList();
            Assert.Single(events);
            var @event = events.LastOrDefault() as PitchCreated;

            Assert.NotNull(@event);
            Assert.Equal(title, @event.Title);
            Assert.Equal(content, @event.Content);
            Assert.Equal(deadLineDate, @event.DeadLineDate);
            Assert.Equal(issueDate, @event.IssueDate);
            Assert.Equal(clientId, @event.ClientId);
            Assert.Equal(ideaId, @event.IdeaId);
            Assert.Equal(pitchAggregate.Id, @event.Id);
        }

        [Given(@"An existing pitch with title ""([^""]*)"", content ""([^""]*)"", dead line date ""([^""]*)"", issue date ""([^""]*)"", client id ""([^""]*)"", idea id ""([^""]*)"" and an owner ""([^""]*)""")]
        public void GivenAnExistingPitchWithTitleContentDeadLineDateIssueDateClientIdIdeaIdAndAnOwner(string title, string content, DateTime? deadLineDate, DateTime? issueDate, string clientId, string ideaId, string ownerId)
        {
            var aggregate = new PitchAggregate(title, content, deadLineDate, issueDate, clientId, ideaId, ownerId);
            aggregate.ClearUncommitedEvents();
            _aggregateContext.Aggregate = aggregate;
        }

        [When(@"A user with id ""([^""]*)"" delete the pitch")]
        public void WhenAUserWithIdDeleteThePitch(string ownerId)
        {
            var pitchAggregate = _aggregateContext.Aggregate as PitchAggregate;

            Assert.NotNull(pitchAggregate);

            pitchAggregate.Delete(ownerId);
        }

        [Then(@"The pitch is deleted")]
        public void ThenThePitchIsDeleted()
        {
            var pitchAggregate = _aggregateContext.Aggregate as PitchAggregate;

            Assert.NotNull(pitchAggregate);
            Assert.True(pitchAggregate.Deleted);

            var events = pitchAggregate.GetUncommitedEvents().ToList();
            Assert.Single(events);
            var @event = events.LastOrDefault() as PitchDeleted;

            Assert.NotNull(@event);
            Assert.Equal(pitchAggregate.Id, @event.Id);
        }

        [Then(@"The pitch is not deleted")]
        public void ThenThePitchIsNotDeleted()
        {
            var pitchAggregate = _aggregateContext.Aggregate as PitchAggregate;

            Assert.NotNull(pitchAggregate);
            Assert.False(pitchAggregate.Deleted);

            Assert.DoesNotContain(pitchAggregate.GetUncommitedEvents(), e => e is PitchDeleted);
        }

    }
}
