using HotChocolate.Authorization;
using HotChocolate.Types;
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
        private readonly IContext _context;
        private readonly IMediator _mediator;

        public PitchesMutations(IContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [Authorize(Roles = new[] { "user" })]
        public async Task<PitchAddedPayload> AddPitchAsync(
    PitchInput pitchInput,
    CancellationToken cancellationToken = default)
        {
            var command = new CreatePitch(pitchInput.Title, pitchInput.Content, pitchInput.DeadLineDate, pitchInput.IssueDate, pitchInput.ClientId, pitchInput.IdeaId, _context.UserId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return new PitchAddedPayload { PitchId = result.Data?.Id };
            }

            //TODO gerer le retour des erreurs

            return new PitchAddedPayload();
        }

        [Authorize(Roles = new[] { "user" })]
        public async Task<string> RemovePitchAsync(
            string id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeletePitch(id, _context.UserId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return id;
            }

            //TODO gerer le retour des erreurs

            return id;
        }

    }
}
