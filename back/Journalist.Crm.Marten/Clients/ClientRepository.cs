using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Pitches.DataModels;
using Marten;
using Marten.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten.Clients
{
    public class ClientRepository : IReadClients
    {
        private readonly IQuerySession _session;

        public ClientRepository(IQuerySession session)
        {
            _session = session;
        }

        public Task<IReadOnlyList<ClientDocument>> AutoCompleteClientasync(string text, string userId, CancellationToken cancellationToken) 
            => _session.Query<ClientDocument>().Where(c => c.UserId == userId && c.Name.Contains(text, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);

        public Task<ClientDocument?> GetClientAsync(string clientId, string userId, CancellationToken cancellationToken) 
            => _session.Query<ClientDocument>().Where(c => c.Id == clientId && c.UserId == userId).FirstOrDefaultAsync(cancellationToken);

        public async Task<ClientResultSet> GetClientsAsync(GetClientsRequest request, CancellationToken cancellationToken = default)
        {
            var query = _session.Query<ClientDocument>().Where(c => c.UserId == request.UserId);

            if (!string.IsNullOrWhiteSpace(request.PitchId))
            {
                query = query.Where(c => c.PitchesIds.Any(p => p == request.PitchId));
            }

            var pagedResult = await query.ToPagedListAsync(request.Skip, request.Take, cancellationToken);

            return new ClientResultSet(pagedResult.ToList(), pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
        }
    }
}
