using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches.DataModels;
using Journalist.Crm.Domain.Pitches.Events;
using Marten;
using Marten.Events.Projections;
using System.Linq;
using System.Threading.Tasks;
using Journalist.Crm.Domain.Pitches;

namespace Journalist.Crm.Marten.Pitches
{
    public class PitchProjection : EventProjection
    {
        public async Task Project(PitchCreated pitchCreated, IDocumentOperations ops)
        {
            ops.Store(new PitchDocument(pitchCreated.Id, pitchCreated.Content, pitchCreated.DeadLineDate, pitchCreated.IssueDate, pitchCreated.ClientId, pitchCreated.IdeaId, pitchCreated.OwnerId, PitchStates.Draft));

            var client = await ops.LoadAsync<ClientDocument>(pitchCreated.ClientId);

            if (client != null && client.PitchesIds.All(id => id != pitchCreated.Id))
            {
                client.PitchesIds.Add(pitchCreated.Id);
                ops.Store(client);
            }

            var idea = await ops.LoadAsync<IdeaDocument>(pitchCreated.IdeaId);

            if (idea != null && idea.PitchesIds.All(id => id != pitchCreated.Id))
            {
                idea.PitchesIds.Add(pitchCreated.Id);
                ops.Store(idea);
            }
        }

        private async Task UpdateStatus(string pitchId, string statusCode, IDocumentOperations ops)
        {
            var pitch = await ops.Query<PitchDocument>().SingleOrDefaultAsync(c => c.Id == pitchId);

            if (pitch != null)
            {
                var pitchUpdated = pitch with { StatusCode = statusCode };

                ops.Store(pitchUpdated);
            }
        }

        public Task Project(PitchCancelled pitchCancelled, IDocumentOperations ops)
            => UpdateStatus(pitchCancelled.Id, PitchStates.Cancelled, ops);
        public Task Project(PitchSent pitchSent, IDocumentOperations ops)
            => UpdateStatus(pitchSent.Id, PitchStates.Sent, ops);
        public Task Project(PitchAccepted pitchAccepted, IDocumentOperations ops)
            => UpdateStatus(pitchAccepted.Id, PitchStates.Accepted, ops);
        public Task Project(PitchRefused pitchRefused, IDocumentOperations ops)
            => UpdateStatus(pitchRefused.Id, PitchStates.Refused, ops);
        public Task Project(PitchReadyToSend pitchReadyToSend, IDocumentOperations ops)
            => UpdateStatus(pitchReadyToSend.Id, PitchStates.ReadyToSend, ops);

        public async Task Project(PitchContentChanged pitchContentChanged, IDocumentOperations ops)
        {
            var pitch = await ops.Query<PitchDocument>().SingleOrDefaultAsync(c => c.Id == pitchContentChanged.Id);

            if (pitch != null)
            {
                var pitchUpdated = pitch with { Content = pitchContentChanged.Content };

                ops.Store(pitchUpdated);
            }
        }

        public async Task Project(PitchDeadLineRescheduled pitchDeadlineRescheduled, IDocumentOperations ops)
        {
            var pitch = await ops.Query<PitchDocument>().SingleOrDefaultAsync(c => c.Id == pitchDeadlineRescheduled.Id);

            if (pitch != null)
            {
                var pitchUpdated = pitch with { DeadLineDate = pitchDeadlineRescheduled.DeadLineDate };

                ops.Store(pitchUpdated);
            }
        }

        public async Task Project(PitchIssueRescheduled pitchIssueRescheduled, IDocumentOperations ops)
        {
            var pitch = await ops.Query<PitchDocument>().SingleOrDefaultAsync(c => c.Id == pitchIssueRescheduled.Id);

            if (pitch != null)
            {
                var pitchUpdated = pitch with { IssueDate = pitchIssueRescheduled.IssueDate };

                ops.Store(pitchUpdated);
            }
        }

        public async Task Project(PitchClientChanged pitchClientChanged, IDocumentOperations ops)
        {
            var pitch = await ops.Query<PitchDocument>().SingleOrDefaultAsync(c => c.Id == pitchClientChanged.Id);

            if (pitch != null)
            {
                var oldClient = await ops.LoadAsync<ClientDocument>(pitch.ClientId);

                if (oldClient != null && oldClient.PitchesIds.Any(id => id == pitchClientChanged.Id))
                {
                    oldClient.PitchesIds.Remove(pitchClientChanged.Id);
                    ops.Store(oldClient);
                }

                var newClient = await ops.LoadAsync<ClientDocument>(pitchClientChanged.ClientId);

                if (newClient != null && newClient.PitchesIds.All(id => id != pitchClientChanged.Id))
                {
                    newClient.PitchesIds.Add(pitchClientChanged.Id);
                    ops.Store(newClient);
                }

                var pitchUpdated = pitch with { ClientId = pitchClientChanged.ClientId };

                ops.Store(pitchUpdated);
            }
        }

        public async Task Project(PitchIdeaChanged pitchIdeaChanged, IDocumentOperations ops)
        {
            var pitch = await ops.Query<PitchDocument>().SingleOrDefaultAsync(c => c.Id == pitchIdeaChanged.Id);

            if (pitch != null)
            {
                var oldIdea = await ops.LoadAsync<IdeaDocument>(pitch.IdeaId);

                if (oldIdea != null && oldIdea.PitchesIds.Any(id => id == pitchIdeaChanged.Id))
                {
                    oldIdea.PitchesIds.Remove(pitchIdeaChanged.Id);
                    ops.Store(oldIdea);
                }

                var newIdea = await ops.LoadAsync<IdeaDocument>(pitchIdeaChanged.IdeaId);

                if (newIdea != null && newIdea.PitchesIds.All(id => id != pitchIdeaChanged.Id))
                {
                    newIdea.PitchesIds.Add(pitchIdeaChanged.Id);
                    ops.Store(newIdea);
                }

                var pitchUpdated = pitch with { IdeaId = pitchIdeaChanged.IdeaId };

                ops.Store(pitchUpdated);
            }
        }
    }
}
