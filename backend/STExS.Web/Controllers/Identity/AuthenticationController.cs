using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers;

public class LoginModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool StayLoggedIn { get; set; }
}

public enum LoginFailureType
{
    Requires2FA = 0,
    LockedOut = 1,
    WrongCredentials = 2,
    NotAllowedToSignIn = 3
}

[ApiController]
[Route("Identity/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger logger;
    private readonly SignInManager<ApplicationUser> signInManager;

    public AuthenticationController(SignInManager<ApplicationUser> signInManager,
        ILogger logger)
    {
        this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(LoginFailureType))]
    public async Task<IActionResult> Login([FromBody] [Required] LoginModel loginModel,
        string? returnUrl)
    {
        var result = await signInManager.PasswordSignInAsync(loginModel.Email, 
            loginModel.Password,
            loginModel.StayLoggedIn,
            false);
        if(result.Succeeded){
            logger.LogInformation("User logged in.");
            return this.Ok();
        }

        if (result.IsLockedOut)
        {
            return this.Unauthorized(LoginFailureType.Requires2FA);
        }

        if (result.IsLockedOut)
        {
            return this.Unauthorized(LoginFailureType.LockedOut);
        }

        if (result.IsNotAllowed)
        {
            return this.Unauthorized(LoginFailureType.NotAllowedToSignIn);
        }

        return this.Unauthorized(LoginFailureType.WrongCredentials);
    }
}