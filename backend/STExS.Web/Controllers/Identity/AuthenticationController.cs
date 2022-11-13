using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
    private readonly IConfiguration Configuration;
    private readonly ILogger logger;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;

    public AuthenticationController(SignInManager<ApplicationUser> signInManager,
        ILogger<AuthenticationController> logger,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(LoginFailureType))]
    public async Task<IActionResult> Login([FromBody] [Required] LoginModel loginModel,
        string? returnUrl)
    {
        var user = await userManager.FindByEmailAsync(loginModel.Email);
        if (user == null)
        {
            logger.LogWarning("User {Email} tried to log in, but the user does not exist.", loginModel.Email);
            return Unauthorized(LoginFailureType.WrongCredentials);
        }

        var result = await signInManager.PasswordSignInAsync(user.UserName,
            loginModel.Password,
            loginModel.StayLoggedIn,
            false);
        if (result.Succeeded)
        {
            logger.LogInformation("User {Email} logged in successfully.", loginModel.Email);
            var token = GenerateJwtToken(user);
            if (token == null) throw new Exception("Could not generate jwt token.");
            return Ok(token);
        }

        if (result.IsLockedOut)
        {
            logger.LogWarning("User {Email} tried to log in, but the user is locked out.", loginModel.Email);
            return Unauthorized(LoginFailureType.Requires2FA);
        }

        if (result.IsLockedOut)
        {
            logger.LogWarning("User {Email} tried to log in, but the user is locked out.", loginModel.Email);
            return Unauthorized(LoginFailureType.LockedOut);
        }

        if (result.IsNotAllowed)
        {
            logger.LogWarning("User {Email} tried to log in, but the user is not allowed to sign in.",
                loginModel.Email);
            return Unauthorized(LoginFailureType.NotAllowedToSignIn);
        }

        logger.LogWarning("User {Email} tried to log in, but the user entered the wrong credentials.",
            loginModel.Email);
        return Unauthorized(LoginFailureType.WrongCredentials);
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(15);

        var token = new JwtSecurityToken(
            Configuration["Jwt:Issuer"],
            Configuration["Jwt:Audience"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}