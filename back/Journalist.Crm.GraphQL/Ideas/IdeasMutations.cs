using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.CommandHandlers;

namespace Journalist.Crm.GraphQL.Ideas;

[ExtendObjectType("Mutation")]
public class IdeasMutations
{
    [Authorize(Roles = new[] { "user" })]
    [Error(typeof(DomainException))]
    public async Task<IdeaAddedPayload> AddIdeaAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
    CreateIdea createIdea,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Ideas.Commands.CreateIdea, Idea>(createIdea.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return new IdeaAddedPayload { IdeaId = result.Id };
    }

    [Authorize(Roles = new[] { "user" })]
    [Error(typeof(DomainException))]
    public async Task<string> RemoveIdeaAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        DeleteIdea deleteIdea,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Ideas.Commands.DeleteIdea, Idea>(deleteIdea.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }

    [Authorize(Roles = new[] { "user" })]
    [Error(typeof(DomainException))]
    [GraphQLName("modifyIdea")]
    public async Task<string> ModifyIdeaAsync([Service] IMediator mediator, [Service] IContext context, ModifyIdea modifyIdea,
    CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Ideas.Commands.ModifyIdea, Idea>(modifyIdea.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }
}
