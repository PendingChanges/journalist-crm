using HotChocolate.Types;
using HotChocolate;
using HotChocolate.Types.Pagination;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain;
using HotChocolate.Authorization;
using System.Collections.Generic;

namespace Journalist.Crm.GraphQL.Clients;

[ExtendObjectType("Query")]
public class ClientsQueries
{
    private readonly IContext _context;

    public ClientsQueries(IContext context)
    {
        _context = context;
    }

    [Authorize(Roles = new[] { "user" })]
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
            clientResultSet.Data.ToClients(),
            pageInfo,
            ct => ValueTask.FromResult((int)clientResultSet.TotalItemCount));

        return collectionSegment;
    }

    [Authorize(Roles = new[] { "user" })]
    [GraphQLName("client")]
    public async Task<Client?> GetClientAsync([Service] IReadClients clientReader, string id, CancellationToken cancellationToken = default)
        => (await clientReader.GetClientAsync(id, _context.UserId, cancellationToken)).ToClientOrNull();

    [Authorize(Roles = new[] { "user" })]
    [GraphQLName("autoCompleteClient")]
    public async Task<IReadOnlyList<Client>> AutoCompleteClientAsync([Service] IReadClients clientReader, string text, CancellationToken cancellationToken = default)
        => (await clientReader.AutoCompleteClientasync(text, _context.UserId, cancellationToken)).ToClients();
}
