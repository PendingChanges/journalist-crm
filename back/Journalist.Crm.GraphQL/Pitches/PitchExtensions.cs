using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.GraphQL.Clients;
using Journalist.Crm.GraphQL.Ideas;
using Journalist.Crm.GraphQL.Pitches.Outputs;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.CQRS;
using Client = Journalist.Crm.GraphQL.Clients.Client;

namespace Journalist.Crm.GraphQL.Pitches;


//TODO: Create dataloaders for all extensions
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

    [Authorize(Roles = new[] { "user" })]
    public async Task<PitchGuards?> GetGuardsAsync(
        [Parent] Pitch pitch,
        [Service] IReadAggregates aggregateReader,
        [Service] IContext context,
        CancellationToken cancellationToken = default)
    => (await aggregateReader.LoadAsync<Domain.Pitches.Pitch>(pitch.Id,ct: cancellationToken)).ToPitchGuardsOrNull();
}
