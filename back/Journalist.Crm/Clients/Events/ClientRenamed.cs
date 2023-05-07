using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Clients.Events
{
    public sealed record ClientRenamed(EntityId Id, string NewName);
}
