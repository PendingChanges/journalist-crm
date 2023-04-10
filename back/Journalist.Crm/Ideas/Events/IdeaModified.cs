namespace Journalist.Crm.Domain.Ideas.Events
{
    public sealed record IdeaModified(string Id, string NewName, string NewDescription);
}
