using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL.Pitches
{
    [ExtendObjectType("Mutation")]
    public class PitchesMutations
    {

        [Authorize(Roles = new[] { "user" })]
        [Error(typeof(DomainException))]
        public async Task<PitchAddedPayload> AddPitchAsync(
                    [Service] IMediator mediator,
        [Service] IContext context,
    PitchInput pitchInput,
    CancellationToken cancellationToken = default)
        {
            var command = new CreatePitch(pitchInput.Title, pitchInput.Content, pitchInput.DeadLineDate, pitchInput.IssueDate, pitchInput.ClientId, pitchInput.IdeaId, context.UserId);

            var result = await mediator.Send(command, cancellationToken);

            return new PitchAddedPayload { PitchId = result.Id };
        }

        [Authorize(Roles = new[] { "user" })]
        [Error(typeof(DomainException))]
        public async Task<string> RemovePitchAsync(
                    [Service] IMediator mediator,
        [Service] IContext context,
            string id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeletePitch(id, context.UserId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Id;
        }
    }
}
