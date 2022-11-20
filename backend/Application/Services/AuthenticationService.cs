namespace Application.Services;

public class AppAuthenticationService : IAppAuthenticationService
{
    // https://www.c-sharpcorner.com/article/jwt-authentication-with-refresh-tokens-in-net-6-0/
}

public interface IAppAuthenticationService
{
    /// <summary>
    ///     Generates and adds the RefreshToken to the user
    ///     If the credentials are valid, claims representing the user are returned.
    ///     Else null is returned.
    /// </summary>
    /// <param name="userId">the user for which the refreshToken is generated</param>
    public Task CreateRefreshToken(Guid userId);

    public Task RevokeRefreshTokenAsync(Guid userId);
    public Task<string> GenerateAccessToken(Guid userId);
}