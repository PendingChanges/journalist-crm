using AutoFixture;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Neo4j.Ideas;
using Neo4j.Driver;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.Neo4j.Clients;

public class IdeasRepositoryShould
{
    private readonly IdeasRepository _ideasRepository;

    public IdeasRepositoryShould()
    {
        IDriver driver = GraphDatabase.Driver(
               "bolt://localhost:7687",
               AuthTokens.Basic("neo4j", "changeit"));

        _ideasRepository = new IdeasRepository(driver);
    }

    [Fact]
    public async Task AddClient_When_InputIsOk()
    {
        //Arrange
        Fixture fixture = new Fixture();
        var input = fixture.Create<IdeaInput>();
        var userId = "test";

        //Act
        var id = await _ideasRepository.AddIdeaAsync(input, userId);

        //Assert
        Assert.NotNull(id);
    }
}
