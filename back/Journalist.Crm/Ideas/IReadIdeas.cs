using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Ideas.DataModels;
using System.Collections.Generic;

namespace Journalist.Crm.Domain.Ideas
{
    public interface IReadIdeas
    {
        Task<IReadOnlyList<IdeaDocument>> AutoCompleteIdeaAsync(string text, string userId, CancellationToken cancellationToken);
        Task<IdeaDocument?> GetIdeaAsync(string ideaId, string userId, CancellationToken cancellationToken = default);
        Task<IdeaResultSet> GetIdeasAsync(GetIdeasRequest request, CancellationToken cancellationToken = default);
    }
}
