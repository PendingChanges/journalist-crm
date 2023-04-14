﻿using Journalist.Crm.Domain.Pitches.ValueObjects;
using System;

namespace Journalist.Crm.GraphQL.Pitches
{
    public record Pitch(string Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId, string UserId);
}
