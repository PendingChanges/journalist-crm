namespace Journalist.Crm.Domain.Pitches.Events
{
    public sealed record PitchDeleted(string Id, string ClientId, string IdeaId);
}
