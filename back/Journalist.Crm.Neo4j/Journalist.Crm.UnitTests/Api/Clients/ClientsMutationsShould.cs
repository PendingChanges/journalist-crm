using AutoFixture;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.GraphQL.Clients;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Journalist.Crm.UnitTests.Api.Clients;

public class ClientsMutationsShould
{
    [Fact]
    public async Task AddClient_When_InputIsOk()
    {
        //Arrange
        Fixture fixture = new Fixture();
        var input = fixture.Create<ClientInput>();
        var id = fixture.Create<string>();
        var contextMock = new Mock<IContext>();
        var clientsMutations = new ClientsMutations(contextMock.Object);
        var clientsWritterMock = new Mock<IWriteClients>();
        clientsWritterMock.Setup(_ => _.AddClientAsync(input, It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(id);

        //Act
        var response = await clientsMutations.AddClientAsync(clientsWritterMock.Object, input);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(id, response.ClientId);
    }
}
