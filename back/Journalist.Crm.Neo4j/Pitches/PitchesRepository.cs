using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using Neo4j.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Neo4j.Pitches
{
    public class PitchesRepository : RepositoryBase, IReadPitches
    {
        public PitchesRepository(IDriver driver) : base(driver)
        {
        }

        public async Task<PitchResultSet> GetPitchesAsync(GetPitchesRequest request, CancellationToken cancellationToken = default)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ExecuteReadAsync(async transaction =>
                {

                    var baseQuery = @"MATCH (u:User { Id: $userId })-[:OWNS_PITCH]->(p:Pitch)";

                    if (!string.IsNullOrWhiteSpace(request.ClientId))
                    {
                        baseQuery = baseQuery + "-[:CLIENT_PITCHED]->(c:Client { Id: $clientId })";
                    }

                    var countQuery = baseQuery
                                    + @"RETURN count(p)";

                    var resultQuery = baseQuery
                                    + @$"RETURN p
                                    ORDER BY p.{GetSortBy(request.SortBy)}
                                    SKIP $skip
                                    LIMIT $take
                                    ";

                    var countResult = await transaction.RunAsync(countQuery,
                        new
                        {
                            userId = request.UserId,
                            clientId = request.ClientId
                        }
                    );

                    var dataResult = await transaction.RunAsync(resultQuery,
                        new
                        {
                            clientId = request.ClientId,
                            userId = request.UserId,
                            skip = request.Skip,
                            take = request.Take
                        }
                    );

                    var totalCount = (await countResult.SingleAsync())[0].As<int>();
                    var data = (await dataResult.ToListAsync()).ToPitches();

                    return new PitchResultSet(data, totalCount, request.Skip + data.Count < totalCount, request.Skip > 0 && data.Count > 0);
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

        public async Task<int> GetPitchesNbAsyncByClientIdAsync(string clientId, string userId)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ExecuteReadAsync<int>(async transaction =>
                {
                    var result = await transaction.RunAsync(@"
                           MATCH (u:User { Id: $userId })-[:OWNS_CLIENT]->(c:Client {Id: $clientId})<-[r:CLIENT_PITCHED]-()
                           return count(r) as pitchNb
                           ",
                        new
                        {
                            clientId,
                            userId
                        }
                    );

                    return (await result.SingleAsync())[0].As<int>();
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }
    }
}
