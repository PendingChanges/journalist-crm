using HotChocolate;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Ideas;
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

    public async Task<IdeaAddedPayload> AddIdeaAsync(
        [Service] IWriteIdeas _ideasWriter,
        IdeaInput ideaInput,
        CancellationToken cancellationToken = default)
    {
        var id = await _ideasWriter.AddIdeaAsync(ideaInput, _context.UserId, cancellationToken);

        return new IdeaAddedPayload { IdeaId = id };
    }

    public async Task<string> RemoveIdeaAsync(
        [Service] IWriteIdeas _ideasWriter,
        string id,
        CancellationToken cancellationToken = default)
    {
        await _ideasWriter.RemoveIdeaAsync(id, _context.UserId, cancellationToken);
        return id;
    }
}
