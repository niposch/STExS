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
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IRoleHelper roleHelper;
    private readonly IAccessService? accessService;

    public UserManagementController(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IRoleHelper roleHelper)
    {
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        this.roleHelper = roleHelper ?? throw new ArgumentNullException(nameof(roleHelper));
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
                res.Add(
                    new UserListModel()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        HighestRoleType =
                            await this.userManager.GetRolesAsync(user),
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed
                    }
                );
            }

            return this.Ok(res);
        }


        return this.Unauthorized();
    }
}