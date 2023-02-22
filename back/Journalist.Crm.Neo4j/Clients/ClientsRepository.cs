using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Neo4j.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Neo4j.Clients;

public class ClientsRepository : RepositoryBase, IWriteClients, IReadClients
{
    public ClientsRepository(IDriver driver) : base(driver) { }

    public async Task<ClientResultSet> GetClientsAsync(GetClientsRequest request, CancellationToken cancellationToken = default)
    {
        var session = _driver.AsyncSession(WithDatabase);
        try
        {
            return await session.ExecuteReadAsync(async transaction =>
            {

                var baseQuery = @"MATCH (u:User { Id: $userId })-[:OWNS_CLIENT]->(c:Client)";

                if (!string.IsNullOrWhiteSpace(request.PitchId))
                {
                    baseQuery = baseQuery + "<-[:CLIENT_PITCHED]-(p:Pitch { Id: $pitchId })";
                }

                var countQuery = baseQuery
                                + @"RETURN count(c)";

                var resultQuery = baseQuery
                                + @$"RETURN c
                                    ORDER BY c.{GetSortBy(request.SortBy)}
                                    SKIP $skip
                                    LIMIT $take
                                    ";

                var countResult = await transaction.RunAsync(countQuery,
                    new
                    {
                        userId = request.UserId,
                        pitchId = request.PitchId
                    }
                );

                var dataResult = await transaction.RunAsync(resultQuery,
                    new
                    {
                        pitchId = request.PitchId,
                        userId = request.UserId,
                        skip = request.Skip,
                        take = request.Take
                    }
                );

                var totalCount = (await countResult.SingleAsync())[0].As<int>();
                var data = (await dataResult.ToListAsync()).ToClients();

                return new ClientResultSet(data, totalCount, request.Skip + data.Count < totalCount, request.Skip > 0 && data.Count > 0);
            });
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    private static string GetSortBy(string sortBy) => sortBy switch
    {
        "Name" => "Name",
        "Id" => "Id",
        _ => "Name"
    };

    public async Task<string> AddClientAsync(ClientInput input, string userId, CancellationToken cancellationToken = default)
    {
        var session = _driver.AsyncSession(WithDatabase);
        try
        {
            return await session.ExecuteWriteAsync(async transaction =>
            {
                var id = Guid.NewGuid().ToString();

                await transaction.RunAsync(@"
                           MATCH (u:User {Id: $userId })
                           CREATE (u)-[:OWNS_CLIENT]->(n:Client {Id: $id, Name: $name});
                           ",
                    new
                    {
                        id,
                        name = input.Name,
                        userId = userId
                    }
                );

                return id;
            });
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public async Task RemoveClientAsync(string id, string userId, CancellationToken cancellationToken = default)
    {
        var session = _driver.AsyncSession(WithDatabase);
        try
        {
            await session.ExecuteWriteAsync(async transaction =>
            {
                await transaction.RunAsync(@"
                           MATCH (u:User { Id: $userId})-[:OWNS_CLIENT]->(n:Client {Id: $id}) DETACH DELETE n
                           ",
                    new
                    {
                        id,
                        userId,
                    }
                );
            });
        }
        finally
        {
            await session.CloseAsync();
        }
    }
}
