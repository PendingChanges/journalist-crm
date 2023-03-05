using HotChocolate.Types;
using HotChocolate;
using HotChocolate.Types.Pagination;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Authorization;

namespace Journalist.Crm.GraphQL.Ideas;

[ExtendObjectType("Query")]
public class IdeasQueries
{
    private readonly IContext _context;

    public IdeasQueries(IContext context)
    {
        _context = context;
    }

    [Authorize(Roles = new[] { "user" })]
    [GraphQLName("allIdeas")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Idea>> GetIdeas(
         [Service] IReadIdeas ideasReader,
            int? skip,
            int? take,
            string? sortBy,
            CancellationToken cancellationToken = default)
    {
        var request = new GetIdeasRequest(null, skip, take, sortBy, _context.UserId);
        var pitchesResultSet = await ideasReader.GetIdeasAsync(request, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(pitchesResultSet.HasNextPage, pitchesResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Idea>(
            pitchesResultSet.Data,
            pageInfo,
            ct => ValueTask.FromResult((int)pitchesResultSet.TotalItemCount));

        return collectionSegment;
    }
}
