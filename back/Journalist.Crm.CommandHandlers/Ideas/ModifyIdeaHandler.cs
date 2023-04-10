﻿using Journalist.Crm.Domain.Ideas.Commands;
using Journalist.Crm.Domain.Ideas;
using System.Threading.Tasks;
using System.Threading;
using Journalist.Crm.Domain;
using MediatR;

namespace Journalist.Crm.CommandHandlers.Ideas
{
    internal class ModifyIdeaHandler : SingleAggregateCommandHandlerBase<ModifyIdea, IdeaAggregate>
    {
        public ModifyIdeaHandler(IStoreAggregates aggregateStore) : base(aggregateStore) { }

        protected override void ExecuteCommand(IdeaAggregate aggregate, ModifyIdea command, string ownerId) => aggregate.Modify(command.NewName, command.NewDescription, ownerId);

        protected override Task<IdeaAggregate?> LoadAggregate(ModifyIdea command, CancellationToken cancellationToken) => _aggregateStore.LoadAsync<IdeaAggregate>(command.Id, ct: cancellationToken);
    }
}
