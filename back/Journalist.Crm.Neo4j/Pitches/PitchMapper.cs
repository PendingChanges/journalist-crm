using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches.DataModels;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Journalist.Crm.Neo4j.Pitches
{
    public static class PitchMapper
    {
        public static Pitch ToPitch(this INode node)
    => new Pitch(
        node.Properties[nameof(Pitch.Id)].As<string>(),
        node.Properties[nameof(Pitch.Title)].As<string>(),
        node.Properties[nameof(Pitch.Content)].As<string>(),
        node.Properties[nameof(Pitch.DeadLineDate)].As<DateTime>(),
        node.Properties[nameof(Pitch.IssueDate)].As<DateTime>(),
        node.Properties[nameof(Pitch.StatusCode)].As<string>()
        );

        public static IReadOnlyCollection<Pitch> ToPitches(this IEnumerable<IRecord> records)
            => records.Select(r => r["p"].As<INode>().ToPitch()).ToList();
    }
}
