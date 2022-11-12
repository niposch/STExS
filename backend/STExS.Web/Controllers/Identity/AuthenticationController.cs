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
    private readonly UserManager<ApplicationUser> userManager;

    public AuthenticationController(SignInManager<ApplicationUser> signInManager,
        ILogger<AuthenticationController> logger,
        UserManager<ApplicationUser> userManager)
    {
        this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(LoginFailureType))]
    public async Task<IActionResult> Login([FromBody] [Required] LoginModel loginModel,
        string? returnUrl)
    {
        var user = await this.userManager.FindByEmailAsync(loginModel.Email);
        if (user == null)
        {
            this.logger.LogWarning("User {Email} tried to log in, but the user does not exist.", loginModel.Email);
            return this.Unauthorized(LoginFailureType.WrongCredentials);
        }
        var result = await signInManager.PasswordSignInAsync(user.UserName, 
            loginModel.Password,
            loginModel.StayLoggedIn,
            false);
        if(result.Succeeded){
            this.logger.LogInformation("User {Email} logged in successfully.", loginModel.Email);
            return this.Ok();
        }

        if (result.IsLockedOut)
        {
            this.logger.LogWarning("User {Email} tried to log in, but the user is locked out.", loginModel.Email);
            return this.Unauthorized(LoginFailureType.Requires2FA);
        }

        if (result.IsLockedOut)
        {
            this.logger.LogWarning("User {Email} tried to log in, but the user is locked out.", loginModel.Email);
            return this.Unauthorized(LoginFailureType.LockedOut);
        }

        if (result.IsNotAllowed)
        {
            this.logger.LogWarning("User {Email} tried to log in, but the user is not allowed to sign in.", loginModel.Email);
            return this.Unauthorized(LoginFailureType.NotAllowedToSignIn);
        }
        this.logger.LogWarning("User {Email} tried to log in, but the user entered the wrong credentials.", loginModel.Email);
        return this.Unauthorized(LoginFailureType.WrongCredentials);
    }
}