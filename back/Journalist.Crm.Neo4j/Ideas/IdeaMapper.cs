using Journalist.Crm.Domain.Ideas.DataModels;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Journalist.Crm.Neo4j.Ideas
{
    public static class IdeaMapper
    {
        public static Idea ToIdea(this INode node)
    => new Idea(
        node.Properties[nameof(Idea.Id)].As<string>(),
        node.Properties[nameof(Idea.Name)].As<string>(),
        node.Properties[nameof(Idea.Name)].As<string?>()
        );

        public static IReadOnlyCollection<Idea> ToIdeas(this IEnumerable<IRecord> records)
            => records.Select(r => r["i"].As<INode>().ToIdea()).ToList();
    }
}
