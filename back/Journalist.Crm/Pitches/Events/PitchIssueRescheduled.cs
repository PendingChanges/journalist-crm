using System;

namespace Journalist.Crm.Domain.Pitches.Events
{
    public record PitchIssueRescheduled(string Id, DateTime? IssueDate);
}
