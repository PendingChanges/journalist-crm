using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.Domain;
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
    [Error(typeof(DomainException))]
    [GraphQLName("addClient")]
    public async Task<ClientAddedPayload> AddClientAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        CreateClientInput clientInput,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateClient(clientInput.Name, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return new ClientAddedPayload { ClientId = result.Id };
    }

    [Authorize(Roles = new[] { "user" })]
    [Error(typeof(DomainException))]
    [GraphQLName("removeClient")]
    public async Task<string> RemoveClientAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        string id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteClient(id, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }

    [Authorize(Roles = new[] { "user" })]
    [Error(typeof(DomainException))]
    [GraphQLName("renameClient")]
    public async Task<string> RenameClientAsync([Service] IMediator mediator, [Service] IContext context, RenameClientInput renameClientInput,
        CancellationToken cancellationToken = default)
    {
        var command = new RenameClient(renameClientInput.Id, renameClientInput.Name, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }
}
