using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches.DataModels;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL.Pitches;


[ExtendObjectType(typeof(Pitch))]
public class PitchExtensions
{
    private readonly IContext _context;

    public PitchExtensions(IContext context)
    {
        _context = context;
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Idea>> GetIdeasAsync(
[Parent] Pitch pitch,
[Service] IReadIdeas ideasReader,
    int? skip,
    int? take,
    string? sortBy,
CancellationToken cancellationToken = default)
    {
        var request = new GetIdeasRequest(pitch.Id, skip, take, sortBy, _context.UserId);
        var pitchesResultSet = await ideasReader.GetIdeasAsync(request, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(pitchesResultSet.HasNextPage, pitchesResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Idea>(
            pitchesResultSet.Data,
            pageInfo,
            ct => ValueTask.FromResult(pitchesResultSet.TotalItemCount));

        return collectionSegment;
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Client>> GetClientsAsync(
[Parent] Pitch pitch,
[Service] IReadClients clientsReader,
int? skip,
int? take,
string? sortBy,
CancellationToken cancellationToken = default)
    {
        var request = new GetClientsRequest(pitch.Id, skip, take, sortBy, _context.UserId);
        var clientsResultSet = await clientsReader.GetClientsAsync(request, cancellationToken);


        var pageInfo = new CollectionSegmentInfo(clientsResultSet.HasNextPage, clientsResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Client>(
            clientsResultSet.Data,
            pageInfo,
            ct => ValueTask.FromResult(clientsResultSet.TotalItemCount));

        return collectionSegment;
    }
}
