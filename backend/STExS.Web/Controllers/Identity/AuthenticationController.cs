using Application.Helper.Roles;
using Application.Interfaces;
using Common.Models.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STExS.Helper;

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
    [Route("modifyMyProfile")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<IdentityError>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateMyProfile(ProfileUpdateItem updateItem, CancellationToken cancellationToken)
    {
        var userId = this.User.GetUserId();
        var user = await this.userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return this.Unauthorized();
        }
        user.FirstName = updateItem.FirstName;
        user.LastName = updateItem.LastName;
        user.PhoneNumber = updateItem.PhoneNumber;
        var confirmEmail = false;
        if (!string.IsNullOrWhiteSpace(updateItem.Email))
        {
            user.Email = updateItem.Email;
            confirmEmail = true;
        }
        user.UserName = updateItem.UserName;
        user.MatrikelNumber = updateItem.MatrikelNumber;
        var result = await this.userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return this.BadRequest(result.Errors);
        }
        if (confirmEmail)
        {
            // TODO implement email confirmation
        }

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