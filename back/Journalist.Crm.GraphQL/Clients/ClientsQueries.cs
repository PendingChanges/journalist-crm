using HotChocolate.Types;
using HotChocolate;
using HotChocolate.Types.Pagination;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using HotChocolate.AspNetCore.Authorization;
using Journalist.Crm.Domain;

namespace Journalist.Crm.GraphQL.Clients;

[ExtendObjectType("Query")]
public class ClientsQueries
{
    private readonly IContext _context;

    public ClientsQueries(IContext context)
    {
        _context = context;
    }

    [Authorize]
    [GraphQLName("allClients")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Client>> GetClientsAcync(
        [Service] IReadClients clientReader,
            int? skip,
            int? take,
            string? sortBy,
            CancellationToken cancellationToken = default
        )
    {
        var request = new GetClientsRequest(null, skip, take, sortBy, _context.UserId);
        var clientResultSet = await clientReader.GetClientsAsync(request, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(clientResultSet.HasNextPage, clientResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Client>(
            clientResultSet.Data,
            pageInfo,
            ct => ValueTask.FromResult((int)clientResultSet.TotalItemCount));

        return collectionSegment;
    }
}
