using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL.Clients;

[ExtendObjectType(typeof(Client))]
public class ClientExtensions
{
    private readonly IContext _context;

    public ClientExtensions(IContext context)
    {
        _context = context;
    }

    public Task<long> NbOfPitchesAsync(
        [Parent] Client client,
        [Service] IReadPitches pitchesReader,
        CancellationToken cancellationToken = default) => pitchesReader.GetPitchesNbAsyncByClientIdAsync(client.Id, _context.UserId, cancellationToken);


    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Pitch>> GetPitchesAsync(
        [Parent] Client client,
        [Service] IReadPitches pitchesReader,
        int? skip,
        int? take,
        string? sortBy,
        CancellationToken cancellationToken = default)
    {
        var request = new GetPitchesRequest(client.Id, skip, take , sortBy, _context.UserId);
        var pitchesResultSet = await pitchesReader.GetPitchesAsync(request, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(pitchesResultSet.HasNextPage, pitchesResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Pitch>(
            pitchesResultSet.Data,
            pageInfo,
            ct => ValueTask.FromResult((int)pitchesResultSet.TotalItemCount));

        return collectionSegment;
    }
}
