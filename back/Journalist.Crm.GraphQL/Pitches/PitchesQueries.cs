﻿using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using Journalist.Crm.Domain;
using HotChocolate.AspNetCore.Authorization;

namespace Journalist.Crm.GraphQL.Pitches;

[ExtendObjectType("Query")]
public class PitchesQueries
{
    private readonly IContext _context;

    public PitchesQueries(IContext context)
    {
        _context = context;
    }

    [Authorize]
    [GraphQLName("allPitches")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Pitch>> GetPitches(
         [Service] IReadPitches pitchesReader,
            int? skip,
    int? take,
    string? sortBy,
            CancellationToken cancellationToken = default)
    {
        var request = new GetPitchesRequest(null, skip, take, sortBy, _context.UserId);
        var pitchesResultSet = await pitchesReader.GetPitchesAsync(request, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(pitchesResultSet.HasNextPage, pitchesResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Pitch>(
            pitchesResultSet.Data,
            pageInfo,
            ct => ValueTask.FromResult(pitchesResultSet.TotalItemCount));

        return collectionSegment;
    }
}
