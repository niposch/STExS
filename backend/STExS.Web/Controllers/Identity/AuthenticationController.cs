using Application.Helper.Roles;
using Application.Interfaces;
using Application.Services;
using Common.Models.Authentication;
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
    public enum SignedInResult
    {
        Success = 0,
        EmailNotConfirmed = 1,
        Failed = 2
    }

    private readonly IEmailService emailService;
    private readonly IRoleHelper roleHelper;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;

    public AuthenticateController(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IRoleHelper roleHelper,
        IEmailService emailService)
    {
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        this.roleHelper = roleHelper ?? throw new ArgumentNullException(nameof(roleHelper));
        this.emailService = emailService;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDetailsModel))]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignedInResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SignedInResult))]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AppLoginModel model)
    {
        var user = await this.userManager.FindByEmailAsync(model.Email);
        if (user == null) return this.Unauthorized();

        var result = await this.signInManager.PasswordSignInAsync(user, model.Password, true, false);
        if (result.IsNotAllowed && !user.EmailConfirmed) return this.BadRequest(SignedInResult.EmailNotConfirmed);
        if (result.Succeeded)
        {
            await this.signInManager.SignInAsync(user, true);
            return this.Ok(SignedInResult.Success);
        }

        return this.Unauthorized(SignedInResult.Failed);
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
        if (user == null) return this.Unauthorized();
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
        if (!result.Succeeded) return this.BadRequest(result.Errors);
        if (confirmEmail) await this.emailService.SendConfirmationEmailAsync(user.Email, await this.userManager.GenerateEmailConfirmationTokenAsync(user), user.Id, cancellationToken);

        return this.Ok();
    }

    [HttpPost]
    [Route("confirmEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string userId, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByIdAsync(userId);
        if (user == null) return this.BadRequest();
        if (user.EmailConfirmed) return this.Ok("Already Confirmed");
        var result = await this.userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded) return this.BadRequest();
        return this.Ok("Email Confirmed");
    }

    [HttpPost]
    [Route("resendConfirmationEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ResendConfirmationEmail([FromQuery] string email, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByEmailAsync(email);
        if (user == null) return this.BadRequest();
        if (user.EmailConfirmed) return this.Ok("Already Confirmed");

        await this.emailService.SendConfirmationEmailAsync(user.Email, await this.userManager.GenerateEmailConfirmationTokenAsync(user), user.Id, cancellationToken);
        return this.Ok("Email Resent");
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] AppRegisterModel model, CancellationToken cancellationToken = default)
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

        await this.userManager.AddToRoleAsync(user, RoleHelper.User);

        await this.emailService.SendConfirmationEmailAsync(user.Email, await this.userManager.GenerateEmailConfirmationTokenAsync(user), user.Id, cancellationToken);

        return this.Ok(new
        {
            Status = "Success",
            Message = "User created successfully!"
        });
    }
}