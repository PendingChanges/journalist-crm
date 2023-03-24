﻿using Journalist.Crm.Domain.Pitches.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Journalist.Crm.GraphQL.Pitchs
{
    public static class PitchMapper
    {
        public static Pitch? ToPitchOrNull(this PitchDocument? pitchDocument)
            => pitchDocument == null ? null : pitchDocument.ToPitch();

        public static Pitch ToPitch(this PitchDocument pitchDocument)
            => new Pitch(pitchDocument.Id, pitchDocument.Title, pitchDocument.Content, pitchDocument.DeadLineDate, pitchDocument.IssueDate, pitchDocument.ClientId, pitchDocument.IdeaId, pitchDocument.UserId);

        public static IReadOnlyList<Pitch> ToPitches(this IReadOnlyList<PitchDocument> clients)
            => clients.Select(ToPitch).ToList();
    }
}