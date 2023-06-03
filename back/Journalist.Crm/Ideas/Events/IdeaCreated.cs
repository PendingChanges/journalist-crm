using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Ideas.Events
{
    public sealed record IdeaCreated(EntityId Id, string Name, string? Description, OwnerId OwnerId);
}
