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
    [Authorize(Roles = new[] { "user" })]
    public async Task<ClientAddedPayload> AddClientAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        ClientInput clientInput,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateClient(clientInput.Name, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return new ClientAddedPayload { ClientId = result.Data?.Id };
        }       

        //TODO gerer le retour des erreurs

        return new ClientAddedPayload();
    }

    [Authorize(Roles = new[] { "user" })]
    public async Task<string> RemoveClientAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        string id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteClient(id, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return id;
        }

        //TODO gerer le retour des erreurs

        return id;
    }
}
