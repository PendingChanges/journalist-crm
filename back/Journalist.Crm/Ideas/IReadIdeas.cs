using Journalist.Crm.Domain.Pitches.DataModels;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Ideas.DataModels;

namespace Journalist.Crm.Domain.Ideas
{
    public interface IReadIdeas
    {
        Task<Idea?> GetIdeaAsync(string ideaId, string userId, CancellationToken cancellationToken = default);
        Task<IdeaResultSet> GetIdeasAsync(GetIdeasRequest request, CancellationToken cancellationToken = default);
    }
}
