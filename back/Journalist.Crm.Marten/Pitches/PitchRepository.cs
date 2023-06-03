using Journalist.Crm.Domain.Ideas.DataModels;
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
            => _session.Query<PitchDocument>().Where(c => c.Id == id && c.OwnerId == userId).FirstOrDefaultAsync(cancellationToken);


        public async Task<PitchResultSet> GetPitchesAsync(GetPitchesRequest request, CancellationToken cancellationToken = default)
        {
            var query = _session.Query<PitchDocument>().Where(p => p.OwnerId == request.UserId);

            if (!string.IsNullOrWhiteSpace(request.ClientId))
            {
                query = query.Where(p => p.ClientId == request.ClientId);
            }

            if (!string.IsNullOrWhiteSpace(request.IdeaId))
            {
                query = query.Where(p => p.IdeaId == request.IdeaId);
            }

            query = query.Where(p => p.StatusCode != PitchStates.Cancelled);

            query = SortBy(request, query);

            var pagedResult = await query.ToPagedListAsync(request.Skip, request.Take, cancellationToken);

            return new PitchResultSet(pagedResult.ToList(), pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
        }

        private static IQueryable<PitchDocument> SortBy(GetPitchesRequest request, IQueryable<PitchDocument> query) => request.SortDirection switch
        {
            "desc" => request.SortBy switch
            {
                _ => query.OrderByDescending(c => c.Content.Title)
            },
            _ => request.SortBy switch
            {
                _ => query.OrderBy(c => c.Content.Title)
            },
        };
    }
}
