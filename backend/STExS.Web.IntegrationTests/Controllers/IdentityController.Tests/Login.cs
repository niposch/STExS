using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JWTRefreshToken.NET6._0.Controllers;

namespace STExS.Web.IntegrationTests.Controllers.IdentityController.Tests;

public sealed class Login : Infrastructure
{
    [Fact]
    public async Task UnauthorizedResponseForNonexistentUser()
    {
        // Arrange

        // Act
        var res = await Client.PostAsJsonAsync("/Identity/Authentication",
            new AppLoginModel
            {
                Email = "superuser@test.com",
                Password = "SuperUser123!"
            });

        // Assert
        var respContents = res.Content.ReadAsStringAsync().Result;
        respContents.Should().Be("2"); // 2 means "wrong credentials" in LoginFailureType
        res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task SucessfullyLogsInWithDefaultUser()
    {
        // Arrange

        // Act
        var res = await Client.PostAsJsonAsync("/Identity/Authentication",
            new AppLoginModel
            {
                Email = DefaultUser.Email,
                Password = "Test333!"
            });

        // Assert
        var respContents = res.Content.ReadAsStringAsync().Result;
        // decode the jwt in respContents
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(respContents);
        jwt.Claims
            .First(c => c.Type == "sub")
            .Value
            .Should()
            .Be(DefaultUser.Id.ToString());
        res.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}