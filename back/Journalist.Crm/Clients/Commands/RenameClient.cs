﻿using MediatR;

namespace Journalist.Crm.Domain.Clients.Commands
{
    public record RenameClient(string Id, string NewName, string OwnerId) : IRequest<ClientAggregate>;
}