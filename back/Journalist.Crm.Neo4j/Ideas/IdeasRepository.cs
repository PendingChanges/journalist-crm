using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Neo4j.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Neo4j.Ideas;

public class IdeasRepository : RepositoryBase, IWriteIdeas, IReadIdeas
{
    public IdeasRepository(IDriver driver) : base(driver) { }

    public async Task<string> AddIdeaAsync(IdeaInput input, string userId, CancellationToken cancellationToken = default)
    {
        var session = _driver.AsyncSession(WithDatabase);
        try
        {
            return await session.ExecuteWriteAsync(async transaction =>
            {
                var id = Guid.NewGuid().ToString();

                await transaction.RunAsync(@"MATCH (u:User {Id: $userId })
                           CREATE (u)-[:OWNS_IDEA]->(n:Idea {Id: $id, Name: $name, Description: $description});
                           ",
                    new
                    {
                        id,
                        name = input.Name,
                        description = input.Description,
                        userId
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

    public async Task<IdeaResultSet> GetIdeasAsync(GetIdeasRequest request, CancellationToken cancellationToken = default)
    {
        var session = _driver.AsyncSession(WithDatabase);
        try
        {
            return await session.ExecuteReadAsync(async transaction =>
            {

                var baseQuery = @"MATCH (u:User { Id: $userId })-[:OWNS_IDEA]->(i:Idea)";

                if (!string.IsNullOrWhiteSpace(request.PitchId))
                {
                    baseQuery = baseQuery + "<-[:IDEA_PITCHED]-(p:Pitch { Id: $pitchId })";
                }

                var countQuery = baseQuery
                                + @"RETURN count(i)";

                var resultQuery = baseQuery
                                + @$"RETURN i
                                    ORDER BY i.{GetSortBy(request.SortBy)}
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
                var data = (await dataResult.ToListAsync()).ToIdeas();

                return new IdeaResultSet(data, totalCount, request.Skip + data.Count < totalCount, request.Skip > 0 && data.Count > 0);
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

    public async Task RemoveIdeaAsync(string id, string userId, CancellationToken cancellationToken = default)
    {
        var session = _driver.AsyncSession(WithDatabase);
        try
        {
            await session.ExecuteWriteAsync(async transaction =>
            {
                await transaction.RunAsync(@"
                           MATCH (u:User { Id: $userId})-[:OWNS_IDEA]->(n:Idea {Id: $id}) DETACH DELETE n
                           ",
                    new
                    {
                        id,
                        userId
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
