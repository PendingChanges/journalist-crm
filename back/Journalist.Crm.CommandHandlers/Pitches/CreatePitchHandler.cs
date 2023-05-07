﻿using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using System.Threading;
using System.Threading.Tasks;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.CommandHandlers.Pitches
{
    internal class CreatePitchHandler : SingleAggregateCommandHandler<CreatePitch, Pitch>
    {
        public CreatePitchHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override AggregateResult ExecuteCommand(Pitch aggregate, CreatePitch command, OwnerId ownerId) => AggregateResult.Create();

        protected override Task<Pitch?> LoadAggregate(CreatePitch command, OwnerId ownerId,
            CancellationToken cancellationToken)
        {
            //TODO : still a problem here, public constructor + what if Create fails?
            var pitch = new Pitch();
            pitch.Create(command.Content, command.DeadLineDate, command.IssueDate, command.ClientId, command.IdeaId, ownerId);
            return Task.FromResult<Pitch?>(pitch);
        }
    }
}
