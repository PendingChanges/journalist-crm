using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches.DataModels;
using Journalist.Crm.GraphQL.Clients;
using Journalist.Crm.GraphQL.Ideas;
using System.Data;
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

    [Authorize(Roles = new[] { "user" })]
    public async Task<Idea?> GetIdeaAsync(
[Parent] Pitch pitch,
[Service] IReadIdeas ideasReader,
CancellationToken cancellationToken = default)
    => (await ideasReader.GetIdeaAsync(pitch.IdeaId, _context.UserId, cancellationToken)).ToIdeaOrNull();

    [Authorize(Roles = new[] { "user" })]
    public async Task<Client?> GetClientAsync(
[Parent] Pitch pitch,
[Service] IReadClients clientsReader,
CancellationToken cancellationToken = default)
    => (await clientsReader.GetClientAsync(pitch.ClientId, _context.UserId, cancellationToken)).ToClientOrNull();
}
