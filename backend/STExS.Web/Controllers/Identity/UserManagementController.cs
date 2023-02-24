using Application.Helper.Roles;
using Application.Interfaces;
using Application.Services.Interfaces;
using Common.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using STExS.Helper;

namespace STExS.Controllers.Identity;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class UserManagementController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IRoleHelper roleHelper;
    private readonly IAccessService accessService;

    public UserManagementController(UserManager<ApplicationUser> userManager,
        IRoleHelper roleHelper,
        IAccessService accessService)
    {
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        this.roleHelper = roleHelper ?? throw new ArgumentNullException(nameof(roleHelper));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
    }

    [HttpGet]
    [Route("UserLists")]
    [Authorize(Roles = $"{RoleHelper.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserListModel>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllUsersProfile()
    {
        var userId = this.User.GetUserId();
        var users = await this.userManager.Users.ToListAsync();
        var res = new List<UserListModel>();
        if (await this.accessService.IsSystemAdmin(userId))
        {
            foreach (var user in users)
            {
                var highestRole = await this.userManager.GetRolesAsync(user) ?? new List<string>();
                res.Add(
                    new UserListModel()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        HighestRoleType =
                            this.roleHelper.GetHighestRole(highestRole.Select(role => this.roleHelper.Parse(role))
                                .Where(r => r != null)
                                .OfType<RoleType>()
                                .ToList()),
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        UserId = user.Id,
                        MatrikelNumber = user.MatrikelNumber
                    }
                );
            }

            return this.Ok(res);
        }


        return this.Unauthorized();
    }
    
    [HttpPost]
    [Route("changeRole")]
    [Authorize(Roles = $"{RoleHelper.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserListModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeRole([FromQuery] RoleType newHighestRole, [FromQuery] Guid userId, CancellationToken cancellationToken = default)
    {
        var currentUserId = this.User.GetUserId();
        
        if (currentUserId == userId)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError); // you cant change your own role
        }
        
        var userToModify = await this.userManager.FindByIdAsync(userId.ToString());
        if (userToModify == null)
        {
            return this.NotFound();
        }

        var roles = await this.userManager.GetRolesAsync(userToModify);
        await this.userManager.RemoveFromRolesAsync(userToModify, roles);
        
        var rolesToAddTo = new List<string>();
        if (newHighestRole == RoleType.Admin)
        {
            rolesToAddTo.Add(RoleHelper.Admin);
            rolesToAddTo.Add(RoleHelper.Teacher);
        }
        else if (newHighestRole == RoleType.Teacher)
        {
            rolesToAddTo.Add(RoleHelper.Teacher);
        }
        rolesToAddTo.Add(RoleHelper.User);
        
        await this.userManager.AddToRolesAsync(userToModify, rolesToAddTo);
        
        return this.Ok();
    }
    
}