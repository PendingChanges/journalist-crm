using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain
{
    public interface IContext
    {
        OwnerId UserId { get; }
    }
}
