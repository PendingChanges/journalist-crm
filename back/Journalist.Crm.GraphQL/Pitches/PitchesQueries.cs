using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using Journalist.Crm.Domain;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Authorization;
using Journalist.Crm.GraphQL.Pitchs;

namespace Journalist.Crm.GraphQL.Pitches;

[ExtendObjectType("Query")]
public class PitchesQueries
{
    private readonly IContext _context;

    public PitchesQueries(IContext context)
    {
        _context = context;
    }

    [Authorize(Roles = new[] { "user" })]
    [GraphQLName("allPitches")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Pitch>> GetPitches(
         [Service] IReadPitches pitchesReader,
            string? clientId,
            string? ideaId,
            int? skip,
    int? take,
    string? sortBy,
            CancellationToken cancellationToken = default)
    {
        var request = new GetPitchesRequest(clientId, ideaId, skip, take, sortBy, _context.UserId);
        var pitchesResultSet = await pitchesReader.GetPitchesAsync(request, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(pitchesResultSet.HasNextPage, pitchesResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Pitch>(
            pitchesResultSet.Data.ToPitches(),
            pageInfo,
            ct => ValueTask.FromResult((int)pitchesResultSet.TotalItemCount));

        return collectionSegment;
    }


    [Authorize(Roles = new[] { "user" })]
    [GraphQLName("pitch")]
    public async Task<Pitch?> GetPitchAsync([Service] IReadPitches pitchesReader, string id, CancellationToken cancellationToken = default)
        => (await pitchesReader.GetPitchAsync(id, _context.UserId, cancellationToken)).ToPitchOrNull();
}
