using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using MediatR;
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
        CreateClient createClient,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<CreateClient, ClientAggregate>(createClient, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return new ClientAddedPayload { ClientId = result.Id };
    }

    [Authorize(Roles = new[] { "user" })]
    [Error(typeof(DomainException))]
    [GraphQLName("removeClient")]
    public async Task<string> RemoveClientAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        DeleteClient deleteClient,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<DeleteClient, ClientAggregate>(deleteClient, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }

    [Authorize(Roles = new[] { "user" })]
    [Error(typeof(DomainException))]
    [GraphQLName("renameClient")]
    public async Task<string> RenameClientAsync([Service] IMediator mediator, [Service] IContext context, RenameClient renameClient,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<RenameClient, ClientAggregate>(renameClient, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }
}
