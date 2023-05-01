using HotChocolate.Types;
using HotChocolate;
using HotChocolate.Types.Pagination;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain;
using HotChocolate.Authorization;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Clients;
using System.Collections.Generic;

namespace Journalist.Crm.GraphQL.Ideas;

[ExtendObjectType("Query")]
public class IdeasQueries
{
    [Authorize(Roles = new[] { "user" })]
    [GraphQLName("allIdeas")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Domain.Ideas.DataModels.Idea>> GetIdeas(
         [Service] IReadIdeas ideasReader,
         [Service] IContext context,
            int? skip,
            int? take,
            string? sortBy,
            string? sortDirection,
            CancellationToken cancellationToken = default)
    {
        var request = new GetIdeasRequest(null, skip, take, sortBy, sortDirection, context.UserId);
        var pitchesResultSet = await ideasReader.GetIdeasAsync(request, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(pitchesResultSet.HasNextPage, pitchesResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Domain.Ideas.DataModels.Idea>(
            pitchesResultSet.Data.ToIdeas(),
            pageInfo,
            ct => ValueTask.FromResult((int)pitchesResultSet.TotalItemCount));

        return collectionSegment;
    }


    [Authorize(Roles = new[] { "user" })]
    [GraphQLName("idea")]
    public async Task<Domain.Ideas.DataModels.Idea?> GetIdeaAsync([Service] IReadIdeas ideasReader, [Service] IContext context, string id, CancellationToken cancellationToken = default)
        => (await ideasReader.GetIdeaAsync(id, context.UserId, cancellationToken)).ToIdeaOrNull();

    [Authorize(Roles = new[] { "user" })]
    [GraphQLName("autoCompleteIdea")]
    public async Task<IReadOnlyList<Domain.Ideas.DataModels.Idea>> AutoCompleteIdeaAsync(
    [Service] IReadIdeas ideaReader,
    [Service] IContext context,
    string text,
    CancellationToken cancellationToken = default)
    => (await ideaReader.AutoCompleteIdeaAsync(text, context.UserId, cancellationToken)).ToIdeas();
}
