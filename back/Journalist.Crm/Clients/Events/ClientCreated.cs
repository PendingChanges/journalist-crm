using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Clients.Events
{
    public sealed record ClientCreated(EntityId Id, string Name, OwnerId OwnerId);
}
