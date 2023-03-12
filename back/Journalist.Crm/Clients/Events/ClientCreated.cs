namespace Journalist.Crm.Domain.Clients.Events
{
    public sealed record ClientCreated(string Id, string Name, string OwnerId);
}
