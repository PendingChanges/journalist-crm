using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Ideas;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL.Ideas;

[ExtendObjectType("Mutation")]
public class IdeasMutations
{
    private readonly IContext _context;

    public IdeasMutations(IContext context)
    {
        _context = context;
    }

    [Authorize(Roles = new[] { "user" })]
    public async Task<IdeaAddedPayload> AddIdeaAsync(
        [Service] IWriteIdeas _ideasWriter,
        IdeaInput ideaInput,
        CancellationToken cancellationToken = default)
    {
        var id = await _ideasWriter.AddIdeaAsync(ideaInput, _context.UserId, cancellationToken);

        return new IdeaAddedPayload { IdeaId = id };
    }

    [Authorize(Roles = new[] { "user" })]
    public async Task<string> RemoveIdeaAsync(
        [Service] IWriteIdeas _ideasWriter,
        string id,
        CancellationToken cancellationToken = default)
    {
        await _ideasWriter.RemoveIdeaAsync(id, _context.UserId, cancellationToken);
        return id;
    }
}
