using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.GraphQL.Ideas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL.Pitches
{
    [ExtendObjectType("Mutation")]
    public class PitchesMutations
    {
        private readonly IContext _context;

        public PitchesMutations(IContext context)
        {
            _context = context;
        }
       
        [Authorize(Roles = new[] { "user" })]
        public async Task<PitchAddedPayload> AddPitchAsync(
    [Service] IWritePitches _pitchWriter,
    PitchInput pitchInput,
    CancellationToken cancellationToken = default)
        {
            var id = await _pitchWriter.AddPitchAsync(pitchInput, _context.UserId, cancellationToken);

            return new PitchAddedPayload { PitchId = id };
        }

        [Authorize(Roles = new[] { "user" })]
        public async Task<string> RemoveIdeaAsync(
            [Service] IWriteIdeas _ideasWriter,
            string id,
            CancellationToken cancellationToken = default)
        {
            await _ideasWriter.RemoveIdeaAsync(id, _context.UserId, cancellationToken);
            return id;
        }

    }
}
