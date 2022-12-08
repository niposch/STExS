using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using Common.Models.Authentication;
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
        var respContents = res.Content.ReadFromJsonAsync<TokenModel>().Result;
        // decode the jwt in respContents
        var handler = new JwtSecurityTokenHandler();
        
        var jwt = handler.ReadJwtToken(respContents!.AccessToken);
        var defaultUser = this.Context.Users.FirstOrDefault();
        jwt.Claims
            .First(c => c.Type == "userId")
            .Value
            .Should()
            .Be(defaultUser!.Id.ToString());
        res.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}