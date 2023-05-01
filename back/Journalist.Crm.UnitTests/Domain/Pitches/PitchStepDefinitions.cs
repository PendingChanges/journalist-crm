using Journalist.Crm.Domain.Common;
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
            var pitchContent = new PitchContent(title, content);
            var aggregate = new Pitch(pitchContent, deadLineDate, issueDate, clientId, ideaId, new OwnerId(ownerId));
            _aggregateContext.Aggregate = aggregate;
        }

        [Then(@"A pitch ""([^""]*)"", content ""([^""]*)"", dead line date ""([^""]*)"", issue date ""([^""]*)"", client id ""([^""]*)"" and idea id ""([^""]*)"" owned by ""([^""]*)"" is created")]
        public void ThenAPitchContentDeadLineDateIssueDateClientIdAndIdeaIdOwnedByIsCreated(string title, string content, DateTime? deadLineDate, DateTime? issueDate, string clientId, string ideaId, string ownerId)
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;
            Assert.NotNull(pitchAggregate);
            Assert.Equal(title, pitchAggregate.Content.Title);
            Assert.Equal(content, pitchAggregate.Content.Summary);
            Assert.Equal(deadLineDate, pitchAggregate.DeadLineDate);
            Assert.Equal(issueDate, pitchAggregate.IssueDate);
            Assert.Equal(clientId, pitchAggregate.ClientId);
            Assert.Equal(ideaId, pitchAggregate.IdeaId);
            Assert.Equal(ownerId, pitchAggregate.OwnerId);

            var events = pitchAggregate.GetUncommittedEvents().ToList();
            Assert.Single(events);
            var @event = events.LastOrDefault() as PitchCreated;

            Assert.NotNull(@event);
            Assert.Equal(title, @event.Content.Title);
            Assert.Equal(content, @event.Content.Summary);
            Assert.Equal(deadLineDate, @event.DeadLineDate);
            Assert.Equal(issueDate, @event.IssueDate);
            Assert.Equal(clientId, @event.ClientId);
            Assert.Equal(ideaId, @event.IdeaId);
            Assert.Equal(pitchAggregate.Id, @event.Id);
        }

        [Given(@"An existing pitch with title ""([^""]*)"", content ""([^""]*)"", dead line date ""([^""]*)"", issue date ""([^""]*)"", client id ""([^""]*)"", idea id ""([^""]*)"" and an owner ""([^""]*)""")]
        public void GivenAnExistingPitchWithTitleContentDeadLineDateIssueDateClientIdIdeaIdAndAnOwner(string title, string content, DateTime? deadLineDate, DateTime? issueDate, string clientId, string ideaId, string ownerId)
        {
            var pitchContent = new PitchContent(title, content);
            var aggregate = new Pitch(pitchContent, deadLineDate, issueDate, clientId, ideaId, new OwnerId(ownerId));
            aggregate.ClearUncommittedEvents();
            _aggregateContext.Aggregate = aggregate;
        }

        [When(@"A user with id ""([^""]*)"" delete the pitch")]
        public void WhenAUserWithIdDeleteThePitch(string ownerId)
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;

            Assert.NotNull(pitchAggregate);

            pitchAggregate.Delete(new OwnerId(ownerId));
        }

        [Then(@"The pitch is deleted")]
        public void ThenThePitchIsDeleted()
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;

            Assert.NotNull(pitchAggregate);
            Assert.True(pitchAggregate.Deleted);

            var events = pitchAggregate.GetUncommittedEvents().ToList();
            Assert.Single(events);
            var @event = events.LastOrDefault() as PitchDeleted;

            Assert.NotNull(@event);
            Assert.Equal(pitchAggregate.Id, @event.Id);
        }

        [Then(@"The pitch is not deleted")]
        public void ThenThePitchIsNotDeleted()
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;

            Assert.NotNull(pitchAggregate);
            Assert.False(pitchAggregate.Deleted);

            Assert.DoesNotContain(pitchAggregate.GetUncommittedEvents(), e => e is PitchDeleted);
        }

        [When(@"A user with id ""([^""]*)"" modify the pitch title ""([^""]*)"", summary ""([^""]*)"", dead line date ""([^""]*)"", issue date ""([^""]*)"", client id ""([^""]*)"", idea id ""([^""]*)""")]
        public void WhenAUserWithIdModifyThePitchTitleSummaryDeadLineDateIssueDateClientIdIdeaIdAndAnOwner(string ownerId, string newPitchTitle, string newPitchSummary, DateTime? newPitchDeadLineDate, DateTime? newPitchIssueDate, string newPitchClientId, string newPitchIdeaId)
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;

            Assert.NotNull(pitchAggregate);

            pitchAggregate.Modify(new PitchContent(newPitchTitle, newPitchSummary), newPitchDeadLineDate, newPitchIssueDate, newPitchClientId, newPitchIdeaId, new OwnerId(ownerId));
        }

        [Then(@"The pitch content change to title ""([^""]*)"" and summary ""([^""]*)""")]
        public void ThenThePitchContentChangeToTitleAndSummary(string newPitchTitle, string newPitchSummary)
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;

            Assert.NotNull(pitchAggregate);
            Assert.Equal(newPitchTitle, pitchAggregate.Content.Title);
            Assert.Equal(newPitchSummary, pitchAggregate.Content.Summary);

            var events = pitchAggregate.GetUncommittedEvents().ToList();
            var @event = events.FirstOrDefault(e => e is PitchContentChanged) as PitchContentChanged;

            Assert.NotNull(@event);
            Assert.Equal(newPitchTitle, @event.Content.Title);
            Assert.Equal(newPitchSummary, @event.Content.Summary);
        }

        [Then(@"The pitch deadline date is rescheduled to ""([^""]*)""")]
        public void ThenThePitchDeadlineDateIsRescheduledTo(DateTime? newPitchDeadLineDate)
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;

            Assert.NotNull(pitchAggregate);
            Assert.Equal(newPitchDeadLineDate, pitchAggregate.DeadLineDate);

            var events = pitchAggregate.GetUncommittedEvents().ToList();
            var @event = events.FirstOrDefault(e => e is PitchDeadLineRescheduled) as PitchDeadLineRescheduled;

            Assert.NotNull(@event);
            Assert.Equal(newPitchDeadLineDate, @event.DeadLineDate);
        }

        [Then(@"The pitch issue date is rescheduled to ""([^""]*)""")]
        public void ThenThePitchIssueDateIsRescheduledTo(DateTime? newPitchIssueDate)
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;

            Assert.NotNull(pitchAggregate);
            Assert.Equal(newPitchIssueDate, pitchAggregate.IssueDate);

            var events = pitchAggregate.GetUncommittedEvents().ToList();
            var @event = events.FirstOrDefault(e => e is PitchIssueRescheduled) as PitchIssueRescheduled;

            Assert.NotNull(@event);
            Assert.Equal(newPitchIssueDate, @event.IssueDate);
        }

        [Then(@"The pitch client change to ""([^""]*)""")]
        public void ThenThePitchClientChangeTo(string newPitchClientId)
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;

            Assert.NotNull(pitchAggregate);
            Assert.Equal(newPitchClientId, pitchAggregate.ClientId);

            var events = pitchAggregate.GetUncommittedEvents().ToList();
            var @event = events.FirstOrDefault(e => e is PitchClientChanged) as PitchClientChanged;

            Assert.NotNull(@event);
            Assert.Equal(newPitchClientId, @event.ClientId);
        }

        [Then(@"The pitch idea change to ""([^""]*)""")]
        public void ThenThePitchIdeaChangeTo(string newPitchIdeaId)
        {
            var pitchAggregate = _aggregateContext.Aggregate as Pitch;

            Assert.NotNull(pitchAggregate);
            Assert.Equal(newPitchIdeaId, pitchAggregate.IdeaId);

            var events = pitchAggregate.GetUncommittedEvents().ToList();
            var @event = events.FirstOrDefault(e => e is PitchIdeaChanged) as PitchIdeaChanged;

            Assert.NotNull(@event);
            Assert.Equal(newPitchIdeaId, @event.IdeaId);
        }

    }
}
