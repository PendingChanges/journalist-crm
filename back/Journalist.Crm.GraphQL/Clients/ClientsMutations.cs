using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL.Clients;

[ExtendObjectType("Mutation")]
public class ClientsMutations
{
    private readonly IContext _context;
    private readonly IMediator _mediator;

    public ClientsMutations(IContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [Authorize(Roles = new[] { "user" })]
    public async Task<ClientAddedPayload> AddClientAsync(
        ClientInput clientInput,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateClient(clientInput.Name, _context.UserId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return new ClientAddedPayload { ClientId = result.Data?.Id };
        }       

        //TODO gerer le retour des erreurs

        return new ClientAddedPayload();
    }

    [Authorize(Roles = new[] { "user" })]
    public async Task<string> RemoveClientAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteClient(id, _context.UserId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return id;
        }

        //TODO gerer le retour des erreurs

        return id;
    }
}
