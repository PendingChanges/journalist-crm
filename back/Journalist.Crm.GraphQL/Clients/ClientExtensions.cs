using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL.Clients;

[ExtendObjectType(typeof(Client))]
public class ClientExtensions
{
    private readonly IContext _context;

    public ClientExtensions(IContext context)
    {
        _context = context;
    }

    [Authorize(Roles = new[] { "user" })]
    public Task<long> NbOfPitchesAsync(
        [Parent] Client client,
        [Service] IReadPitches pitchesReader,
        CancellationToken cancellationToken = default) => pitchesReader.GetPitchesNbByClientIdAsync(client.Id, _context.UserId, cancellationToken);
}
