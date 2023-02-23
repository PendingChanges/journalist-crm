using AutoFixture;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Neo4j.Clients;
using Neo4j.Driver;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.Neo4j.Clients;

public class ClientsRepositoryShould
{
    private readonly ClientsRepository _clientsRepository;

    public ClientsRepositoryShould()
    {
        IDriver driver = GraphDatabase.Driver(
               "bolt://localhost:7687",
               AuthTokens.Basic("neo4j", "changeit"));

        _clientsRepository = new ClientsRepository(driver);
    }

    [Fact]
    public async Task AddClient_When_InputIsOk()
    {
        //Arrange
        Fixture fixture = new Fixture();
        var input = fixture.Create<ClientInput>();

        //Act
        var id = await _clientsRepository.AddClientAsync(input, "test");

        //Assert
        Assert.NotNull(id);
    }

    [Fact]
    public async Task GetClients()
    {
        //Arrange
        var userId = "test";

        //Act 
        var clientResultSet = await _clientsRepository.GetClientsAsync(new Domain.Clients.DataModels.GetClientsRequest(null, 0, 10, "name", userId));

        Assert.NotNull(clientResultSet);
    }

    [Fact]
    public async Task GetClientsByPitch()
    {
        //Arrange
        var userId = "test";

        //Act 
        var clientResultSet = await _clientsRepository.GetClientsAsync(new Domain.Clients.DataModels.GetClientsRequest("1", 0, 10, "name", userId));

        Assert.NotNull(clientResultSet);
    }

    [Fact]
    public async Task RemoveClientAsync()
    {
        //Arrange
        Fixture fixture = new Fixture();
        var input = fixture.Create<ClientInput>();
        var userId = "test";

        //Act
        var id = await _clientsRepository.AddClientAsync(input, userId);

        await _clientsRepository.RemoveClientAsync(id, userId);
        //Assert
    }
}
