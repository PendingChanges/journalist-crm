using System;

namespace Journalist.Crm.Domain.Pitches.Events
{
    public record PitchDeadLineRescheduled(string Id, DateTime? DeadLineDate);
}
