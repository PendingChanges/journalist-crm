using Marten.Events.Projections;
using Marten;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Ideas.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten.Ideas
{
    public class IdeaProjection : EventProjection
    {
        public IdeaDocument Create(IdeaCreated ideaCreated)
            => new(ideaCreated.Id, ideaCreated.Name, ideaCreated.Description, ideaCreated.OwnerId, new List<string>());

        public void Project(IdeaDeleted ideaDeleted, IDocumentOperations ops)
            => ops.Delete<IdeaDocument>(ideaDeleted.Id);

        public async Task Project(IdeaModified @event, IDocumentOperations ops)
        {
            var idea = await ops.Query<IdeaDocument>().SingleAsync(c => c.Id == @event.Id);

            if (idea != null)
            {
                var ideaUpdated = idea with { Name = @event.NewName, Description = @event.NewDescription };

                ops.Store(ideaUpdated);
            }
        }
    }
}
