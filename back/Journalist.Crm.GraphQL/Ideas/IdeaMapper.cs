using Journalist.Crm.Domain.Ideas.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Journalist.Crm.GraphQL.Ideas
{
    public static class IdeaMapper
    {
        public static Idea? ToIdeaOrNull(this IdeaDocument? ideaDocument)
            => ideaDocument == null ? null : ideaDocument.ToIdea();

        public static Idea ToIdea(this IdeaDocument ideaDocument)
            => new Idea(ideaDocument.Id, ideaDocument.Name, ideaDocument.Description, ideaDocument.UserId, ideaDocument.PitchesIds.Count);

        public static IReadOnlyList<Idea> ToIdeas(this IReadOnlyList<IdeaDocument> ideas)
            => ideas.Select(ToIdea).ToList();
    }
}
