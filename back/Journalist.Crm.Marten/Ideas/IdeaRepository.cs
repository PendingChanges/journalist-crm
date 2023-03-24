using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches.DataModels;
using Marten;
using Marten.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten.Ideas
{
    public class IdeaRepository : IReadIdeas
    {
        private readonly IDocumentSession _session;

        public IdeaRepository(IDocumentSession session)
        {
            _session = session;
        }

        public Task<IReadOnlyList<IdeaDocument>> AutoCompleteIdeaasync(string text, string userId, CancellationToken cancellationToken) 
            => _session.Query<IdeaDocument>().Where(c => c.UserId == userId && c.Name.Contains(text, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);

        public Task<IdeaDocument?> GetIdeaAsync(string ideaId, string userId, CancellationToken cancellationToken) 
            => _session.Query<IdeaDocument>().Where(c => c.Id == ideaId && c.UserId == userId).FirstOrDefaultAsync(cancellationToken);

        public async Task<IdeaResultSet> GetIdeasAsync(GetIdeasRequest request, CancellationToken cancellationToken = default)
        {
            var query = _session.Query<IdeaDocument>().Where(c => c.UserId == request.UserId);

            if (!string.IsNullOrWhiteSpace(request.PitchId))
            {
                query = query.Where(c => c.PitchesIds.Any(p => p == request.PitchId));
            }

            var pagedResult = await query.ToPagedListAsync(request.Skip, request.Take, cancellationToken);

            return new IdeaResultSet(pagedResult.ToList(), pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
        }
    }
}
