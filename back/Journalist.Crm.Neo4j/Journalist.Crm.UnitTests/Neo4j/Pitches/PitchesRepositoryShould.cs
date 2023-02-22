using Journalist.Crm.Neo4j.Pitches;
using Neo4j.Driver;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.Neo4j.Pitches;

public class PitchesRepositoryShould
{
    private readonly PitchesRepository _pitchesRepository;

    public PitchesRepositoryShould()
    {
        IDriver driver = GraphDatabase.Driver(
               "bolt://localhost:7687",
               AuthTokens.Basic("neo4j", "changeit"));

        _pitchesRepository = new PitchesRepository(driver);
    }

    [Fact]
    public async Task GetPitchesNbAsyncByClientIdAsync()
    {
        //Arrange
        var clientId = "1";
        var userId = "test";

        //Act
        var nb = await _pitchesRepository.GetPitchesNbAsyncByClientIdAsync(clientId, userId);

        //Assert
        Assert.NotEqual(0, nb);
    }
}
