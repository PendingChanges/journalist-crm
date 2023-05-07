using Journalist.Crm.Domain.Ideas.DataModels;
using System.Collections.Generic;
using System.Linq;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.GraphQL.Ideas
{
    public static class IdeaMapper
    {
        public static Idea? ToIdeaOrNull(this IdeaDocument? ideaDocument)
            => ideaDocument?.ToIdea();

        public static Idea ToIdea(this IdeaDocument ideaDocument) =>
            new(ideaDocument.Id, ideaDocument.Name, ideaDocument.Description, ideaDocument.OwnerId,
                ideaDocument.PitchesIds.Count);

        public static IReadOnlyList<Idea> ToIdeas(this IReadOnlyList<IdeaDocument> ideas)
            => ideas.Select(ToIdea).ToList();

        public static Domain.Ideas.Commands.CreateIdea ToCommand(this CreateIdea createIdea)
            => new(createIdea.Name, createIdea.Description);

        public static Domain.Ideas.Commands.DeleteIdea ToCommand(this DeleteIdea deleteIdea)
            => new(new EntityId(deleteIdea.Id));

        public static Domain.Ideas.Commands.ModifyIdea ToCommand(this ModifyIdea modifyIdea)
            => new(new EntityId(modifyIdea.Id), modifyIdea.NewName, modifyIdea.NewDescription);
    }
}
