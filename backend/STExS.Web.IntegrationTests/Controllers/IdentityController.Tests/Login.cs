using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using STExS.Controllers;

namespace STExS.Web.IntegrationTests;

public sealed class Login:Infrastructure
{
    [Fact]
    public async Task Test1()
    {
        // Arrange
        
        // Act
        var res = await this.Client.PostAsJsonAsync("/Identity/Authentication", new LoginModel
        {
            Email = "superuser@test.com",
            Password = "SuperUser123!",
            StayLoggedIn = false
        });

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}