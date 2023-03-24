using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients.Commands;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Ideas;
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
    private readonly IContext _context;
    private readonly IMediator _mediator;

    public IdeasMutations(IContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [Authorize(Roles = new[] { "user" })]
    public async Task<IdeaAddedPayload> AddIdeaAsync(
    IdeaInput ideaInput,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateIdea(ideaInput.Name, ideaInput.Description, _context.UserId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return new IdeaAddedPayload { IdeaId = result.Data?.Id };
        }

        //TODO gerer le retour des erreurs

        return new IdeaAddedPayload();
    }

    [Authorize(Roles = new[] { "user" })]
    public async Task<string> RemoveIdeaAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteIdea(id, _context.UserId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return id;
        }

        //TODO gerer le retour des erreurs

        return id;
    }
}
