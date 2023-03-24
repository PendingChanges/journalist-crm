using AutoFixture;
using Journalist.Crm.Domain;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.GraphQL.Clients;
using MediatR;
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
        var mediatorMock = new Mock<IMediator>();
        var clientsMutations = new ClientsMutations(contextMock.Object, mediatorMock.Object);

        //Act
        var response = await clientsMutations.AddClientAsync(input);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(id, response.ClientId);
    }
}
