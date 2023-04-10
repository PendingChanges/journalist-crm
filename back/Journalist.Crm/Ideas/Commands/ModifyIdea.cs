namespace Journalist.Crm.Domain.Ideas.Commands
{
    public record ModifyIdea(string Id, string NewName, string NewDescription) : ICommand;
}
