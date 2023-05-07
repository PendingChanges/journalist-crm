using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Clients.Events
{
    public sealed record ClientCreated(EntityId Id, string Name, OwnerId OwnerId);
}
