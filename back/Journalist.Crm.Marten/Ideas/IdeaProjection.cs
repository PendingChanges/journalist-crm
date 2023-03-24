using Marten.Events.Projections;
using Marten;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Ideas.Events;
using System.Collections.Generic;

namespace Journalist.Crm.Marten.Ideas
{
    public class IdeaProjection : EventProjection
    {
        public IdeaDocument Create(IdeaCreated ideaCreated)
            => new IdeaDocument(ideaCreated.Id, ideaCreated.Name, ideaCreated.Description, ideaCreated.OwnerId, new List<string>());

        public void Project(IdeaDeleted ideaDeleted, IDocumentOperations ops)
            => ops.Delete<IdeaDocument>(ideaDeleted.Id);
    }
}
