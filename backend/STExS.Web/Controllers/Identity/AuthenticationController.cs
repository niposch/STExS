using Application.Helper.Roles;
using Application.Interfaces;
using Common.Models.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers.Identity;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthenticateController : ControllerBase
{
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IRoleHelper roleHelper;

    public AuthenticateController(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IRoleHelper roleHelper)
    {
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        this.roleHelper = roleHelper ?? throw new ArgumentNullException(nameof(roleHelper));
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await this.signInManager.SignOutAsync(); 
        return this.Ok();
    }
    
    [HttpGet]
    [Route("userDetails")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(UserDetailsModel))]
    public async Task<IActionResult> UserDetails()
    {
        var user = await this.userManager.GetUserAsync(this.User);
        var roles = await this.userManager.GetRolesAsync(user);
        var res = new UserDetailsModel
        {
            User = user,
            Roles = roles?
                .Select(r => this.roleHelper.Parse(r))
                .Where(r => r != null)
                .OfType<RoleType>()
                .ToList() 
            ?? new List<RoleType>()
        };
        return this.Ok(res);
    }
    
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AppLoginModel model)
    {
        var user = await this.userManager.FindByEmailAsync(model.Email);
        if (user == null )
        {
            return this.Unauthorized();
        }
        
        await this.signInManager.SignInAsync(user, new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddDays(1),
            AllowRefresh = true
        });
        return this.Ok();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] AppRegisterModel model)
    {
        var userExists = await this.userManager.FindByNameAsync(model.UserName);
        if (userExists != null)
            return this.StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Status = "Error",
                Message = "User already exists!"
            });

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName,
            MatrikelNumber = model.MatrikelNumber,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        var result = await this.userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return this.StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Status = "Error",
                Message = "User creation failed! Please check user details and try again."
            });

        await this.userManager.AddToRoleAsync(user, "user");
        
        return this.Ok(new
        {
            Status = "Success",
            Message = "User created successfully!"
        });
    }
}

public class AppRegisterModel
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MatrikelNumber { get; set; } = string.Empty;
}

public class AppLoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserDetailsModel
{
    public ApplicationUser User { get; set; }
    public List<RoleType> Roles { get; set; }
}