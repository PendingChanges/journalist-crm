using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Clients.Events
{
    public sealed record ClientRenamed(EntityId Id, string NewName);
}
