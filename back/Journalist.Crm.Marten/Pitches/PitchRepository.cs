using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using Marten;
using Marten.Pagination;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten.Pitches
{
    public class PitchRepository : IReadPitches
    {
        private readonly IDocumentSession _session;

        public PitchRepository(IDocumentSession session)
        {
            _session = session;
        }

        public Task<PitchDocument?> GetPitchAsync(string id, string userId, CancellationToken cancellationToken = default)
            => _session.Query<PitchDocument>().Where(c => c.Id == id && c.UserId == userId).FirstOrDefaultAsync(cancellationToken);


        public async Task<PitchResultSet> GetPitchesAsync(GetPitchesRequest request, CancellationToken cancellationToken = default)
        {
            var query = _session.Query<PitchDocument>().Where(p => p.UserId == request.UserId);

            if (!string.IsNullOrWhiteSpace(request.ClientId))
            {
                query = query.Where(p => p.ClientId == request.ClientId);
            }

            if (!string.IsNullOrWhiteSpace(request.IdeaId))
            {
                query = query.Where(p => p.IdeaId == request.IdeaId);
            }

            var pagedResult = await query.ToPagedListAsync(request.Skip, request.Take, cancellationToken);

            return new PitchResultSet(pagedResult.ToList(), pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
        }

        public Task<int> GetPitchesNbByClientIdAsync(string clientId, string userId, CancellationToken cancellationToken = default) 
            => _session.Query<PitchDocument>().Where(p => p.ClientId == clientId && p.UserId == userId).CountAsync(cancellationToken);

        public Task<int> GetPitchesNbByIdeaIdAsync(string ideaId, string userId, CancellationToken cancellationToken)
            => _session.Query<PitchDocument>().Where(p => p.IdeaId == ideaId && p.UserId == userId).CountAsync(cancellationToken);
    }
}
