using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches.DataModels;
using Journalist.Crm.Domain.Pitches.Events;
using Marten;
using Marten.Events.Projections;
using System.Linq;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten.Pitches
{
    public class PitchProjection : EventProjection
    {
        public async Task Project(PitchCreated pitchCreated, IDocumentOperations ops)
        {
            ops.Store(new PitchDocument(pitchCreated.Id, pitchCreated.Title, pitchCreated.Content, pitchCreated.DeadLineDate, pitchCreated.IssueDate, pitchCreated.ClientId, pitchCreated.IdeaId, pitchCreated.OwnerId));

            var client = await ops.LoadAsync<ClientDocument>(pitchCreated.ClientId);

            if (client != null && !client.PitchesIds.Any(id => id == pitchCreated.Id))
            {
                client.PitchesIds.Add(pitchCreated.Id);
                ops.Store(client);
            }

            var idea = await ops.LoadAsync<IdeaDocument>(pitchCreated.IdeaId);

            if (idea != null && !idea.PitchesIds.Any(id => id == pitchCreated.Id))
            {
                idea.PitchesIds.Add(pitchCreated.Id);
                ops.Store(idea);
            }
        }

        public async Task Project(PitchDeleted pitchDeleted, IDocumentOperations ops)
        {
            ops.Delete<PitchDocument>(pitchDeleted.Id);

            var client = await ops.LoadAsync<ClientDocument>(pitchDeleted.ClientId);

            if (client != null && client.PitchesIds.Any(id => id == pitchDeleted.Id))
            {
                client.PitchesIds.Remove(pitchDeleted.Id);
                ops.Store(client);
            }

            var idea = await ops.LoadAsync<IdeaDocument>(pitchDeleted.IdeaId);

            if (idea != null && idea.PitchesIds.Any(id => id == pitchDeleted.Id))
            {
                idea.PitchesIds.Add(pitchDeleted.Id);
                ops.Store(idea);
            }
        }
    }
}
