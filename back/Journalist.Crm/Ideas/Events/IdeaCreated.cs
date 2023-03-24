namespace Journalist.Crm.Domain.Ideas.Events
{
    public sealed record IdeaCreated(string Id, string Name, string? Description, string OwnerId);
}
