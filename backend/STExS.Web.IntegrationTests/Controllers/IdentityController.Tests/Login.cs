using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using STExS.Controllers;
using TestHelper;

namespace STExS.Web.IntegrationTests.Controllers.IdentityController.Tests;

public sealed class Login : Infrastructure
{
    [Fact]
    public async Task UnauthorizedResponseForNonexistentUser()
    {
        // Arrange

        // Act
        var res = await Client.PostAsJsonAsync("/Identity/Authentication", 
            new LoginModel
            {
                Email = "superuser@test.com",
                Password = "SuperUser123!",
                StayLoggedIn = false
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
            new LoginModel
            {
                Email = this.DefaultUser.Email,
                Password = "Test333!",
                StayLoggedIn = false
            });

        // Assert
        var respContents = res.Content.ReadAsStringAsync().Result;
        // decode the jwt in respContents
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(respContents);
        jwt.Claims
            .Where(c => c.Type == "sub")
            .First()
            .Value
            .Should()
            .Be(this.DefaultUser.Id);
        res.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}