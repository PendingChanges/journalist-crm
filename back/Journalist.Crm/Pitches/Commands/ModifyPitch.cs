﻿using System;
using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Pitches.Commands
{
    public record ModifyPitch(EntityId Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, string ClientId, string IdeaId) : ICommand;
}
