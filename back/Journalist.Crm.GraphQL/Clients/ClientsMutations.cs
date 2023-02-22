﻿using HotChocolate;
using HotChocolate.Types;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL.Clients;

[ExtendObjectType("Mutation")]
public class ClientsMutations
{
    private readonly IContext _context;

    public ClientsMutations(IContext context)
    {
        _context = context;
    }

    public async Task<ClientAddedPayload> AddClientAsync(
        [Service] IWriteClients _clientWriter,
        ClientInput clientInput,
        CancellationToken cancellationToken = default)
    {

        var id = await _clientWriter.AddClientAsync(clientInput, _context.UserId, cancellationToken);

        return new ClientAddedPayload { ClientId = id };
    }

    public async Task<string> RemoveClientAsync(
        [Service] IWriteClients _clientWriter,
        string id,
        CancellationToken cancellationToken = default)
    {
        await _clientWriter.RemoveClientAsync(id, _context.UserId, cancellationToken);
        return id;
    }
}
