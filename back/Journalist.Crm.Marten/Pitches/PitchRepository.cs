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
        private readonly IQuerySession _session;

        public PitchRepository(IQuerySession session)
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
    }
}
