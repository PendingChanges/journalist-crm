namespace Journalist.Crm.Domain.Pitches.Events
{
    public sealed record PitchCancelled(string Id, string ClientId, string IdeaId);
}
