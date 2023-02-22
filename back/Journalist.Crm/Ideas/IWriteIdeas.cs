using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Ideas;

public interface IWriteIdeas
{
    Task<string> AddIdeaAsync(IdeaInput input, string userId, CancellationToken cancellationToken = default);
    Task RemoveIdeaAsync(string id,  string userId, CancellationToken cancellationToken = default);
}