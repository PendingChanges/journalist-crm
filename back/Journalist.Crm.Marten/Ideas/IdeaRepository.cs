﻿using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
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
        private readonly IQuerySession _session;

        public IdeaRepository(IQuerySession session)
        {
            _session = session;
        }

        public Task<IReadOnlyList<IdeaDocument>> AutoCompleteIdeaAsync(string text, string userId, CancellationToken cancellationToken)
            => _session.Query<IdeaDocument>().Where(c => c.OwnerId == userId && c.Name.Contains(text, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);

        public Task<IdeaDocument?> GetIdeaAsync(string ideaId, string userId, CancellationToken cancellationToken)
            => _session.Query<IdeaDocument>().Where(c => c.Id == ideaId && c.OwnerId == userId).FirstOrDefaultAsync(cancellationToken);

        public async Task<IdeaResultSet> GetIdeasAsync(GetIdeasRequest request, CancellationToken cancellationToken = default)
        {
            var query = _session.Query<IdeaDocument>().Where(c => c.OwnerId == request.UserId);

            if (!string.IsNullOrWhiteSpace(request.PitchId))
            {
                query = query.Where(c => c.PitchesIds.Any(p => p == request.PitchId));
            }

            query = SortBy(request, query);

            var pagedResult = await query.ToPagedListAsync(request.Skip, request.Take, cancellationToken);

            return new IdeaResultSet(pagedResult.ToList(), pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
        }

        private static IQueryable<IdeaDocument> SortBy(GetIdeasRequest request, IQueryable<IdeaDocument> query) => request.SortDirection switch
        {
            "desc" => request.SortBy switch
            {
                _ => query.OrderByDescending(c => c.Name)
            },
            _ => request.SortBy switch
            {
                _ => query.OrderBy(c => c.Name)
            },
        };
    }
}
