using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using Common.Models.Authentication;
using FluentAssertions;
using STExS.Controllers.Identity;

namespace STExS.Web.IntegrationTests.Controllers.IdentityController.Tests;

public sealed class Login : Infrastructure
{
    [Fact]
    public async Task UnauthorizedResponseForNonexistentUser()
    {
        // Arrange

        // Act
        var res = await Client.PostAsJsonAsync("api/Authenticate/login",
            new AppLoginModel
            {
                Email = "irgendwas_anderes@test.com",
                Password = "SuperUser333!"
            });

        // Assert
        var respContents = res.Content.ReadAsStringAsync().Result;
        res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task SucessfullyLogsInWithDefaultUser()
    {
        // Arrange

        // Act
        var res = await Client.PostAsJsonAsync("api/Authenticate/login",
            new AppLoginModel
            {
                Email = "dev@test.com",
                Password = "Test333!"
            });

        // Assert
        res.Headers.Should().Contain(h => h.Key == "Set-Cookie");
    }
}