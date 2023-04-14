using Journalist.Crm.Domain.Pitches.ValueObjects;

namespace Journalist.Crm.Domain.Pitches.Events
{
    public record PitchContentChanged(string Id, PitchContent Content);
}
