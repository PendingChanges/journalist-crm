using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.GraphQL.Clients;
using Journalist.Crm.GraphQL.Ideas;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Client = Journalist.Crm.GraphQL.Clients.Client;

namespace Journalist.Crm.GraphQL.Pitches;


[ExtendObjectType(typeof(Pitch))]
public class PitchExtensions
{
    [Authorize(Roles = new[] { "user" })]
    public async Task<Domain.Ideas.DataModels.Idea?> GetIdeaAsync(
[Parent] Pitch pitch,
[Service] IReadIdeas ideasReader,
[Service] IContext context,
CancellationToken cancellationToken = default)
    => (await ideasReader.GetIdeaAsync(pitch.IdeaId, context.UserId, cancellationToken)).ToIdeaOrNull();

    [Authorize(Roles = new[] { "user" })]
    public async Task<Client?> GetClientAsync(
[Parent] Pitch pitch,
[Service] IReadClients clientsReader,
[Service] IContext context,
CancellationToken cancellationToken = default)
    => (await clientsReader.GetClientAsync(pitch.ClientId, context.UserId, cancellationToken)).ToClientOrNull();
}
