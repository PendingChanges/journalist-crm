using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients.Commands;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.GraphQL.Clients;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.Ideas.Commands;
using MediatR;

namespace Journalist.Crm.GraphQL.Ideas;

[ExtendObjectType("Mutation")]
public class IdeasMutations
{
    [Authorize(Roles = new[] { "user" })]
    [Error(typeof(DomainException))]
    public async Task<IdeaAddedPayload> AddIdeaAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
    IdeaInput ideaInput,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateIdea(ideaInput.Name, ideaInput.Description, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return new IdeaAddedPayload { IdeaId = result.Id };
    }

    [Authorize(Roles = new[] { "user" })]
    [Error(typeof(DomainException))]
    public async Task<string> RemoveIdeaAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        string id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteIdea(id, context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }
}
