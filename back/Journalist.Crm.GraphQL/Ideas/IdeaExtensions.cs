using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL.Clients;

[ExtendObjectType(typeof(Idea))]
public class IdeaExtensions
{
    private readonly IContext _context;

    public IdeaExtensions(IContext context)
    {
        _context = context;
    }

    [Authorize(Roles = new[] { "user" })]
    public Task<long> NbOfPitchesAsync(
        [Parent] Idea idea,
        [Service] IReadPitches pitchesReader,
        CancellationToken cancellationToken = default) => pitchesReader.GetPitchesNbByIdeaIdAsync(idea.Id, _context.UserId, cancellationToken);
}
