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
    public Task<Idea?> GetIdeaAsync(
[Parent] Pitch pitch,
[Service] IReadIdeas ideasReader,
CancellationToken cancellationToken = default)
    => ideasReader.GetIdeaAsync(pitch.IdeaId, _context.UserId, cancellationToken);

    [Authorize(Roles = new[] { "user" })]
    public Task<Client?> GetClientAsync(
[Parent] Pitch pitch,
[Service] IReadClients clientsReader,
CancellationToken cancellationToken = default)
    => clientsReader.GetClientAsync(pitch.ClientId, _context.UserId, cancellationToken);
}
