using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
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
    CreatePitch createPitch,
    CancellationToken cancellationToken = default)
        {
            var command = new WrappedCommand<CreatePitch, PitchAggregate>(createPitch, context.UserId);

            var result = await mediator.Send(command, cancellationToken);

            return new PitchAddedPayload { PitchId = result.Id };
        }

        [Authorize(Roles = new[] { "user" })]
        [Error(typeof(DomainException))]
        public async Task<string> RemovePitchAsync(
                    [Service] IMediator mediator,
        [Service] IContext context,
            DeletePitch deletePitch,
            CancellationToken cancellationToken = default)
        {
            var command = new WrappedCommand<DeletePitch, PitchAggregate>(deletePitch, context.UserId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Id;
        }
    }
}
