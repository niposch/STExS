using Application.DTOs.UserAdmin;
using Application.Helper.Roles;
using Common.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace STExS.Controllers.UserManagement;
[ApiController]
[Route("api/[controller]")]

public class UserManagementController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;

    public UserManagementController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }
    [HttpGet("all")]
    [Authorize(Roles = $"{RoleHelper.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserListItem>))]
    
    public async Task<IActionResult> GetAllUsers()
    {
        var userListItems = new List<UserListItem>();
        foreach (var user in this.userManager.Users)
        {
            var listItem = new UserListItem
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                MatrikelNummer = user.MatrikelNumber,
                PhoneNumber = user.PhoneNumber,
                UserId = user.Id,
                UserName = user.UserName
            };
            listItem.CurrentRole = RoleType.User;
            var roles = await this.userManager.GetRolesAsync(user);
            if (roles.Contains(RoleHelper.Teacher))
            {
                listItem.CurrentRole = RoleType.Teacher;
            }
            if (roles.Contains(RoleHelper.Admin))
            {
                listItem.CurrentRole = RoleType.Admin;
            }
            userListItems.Add(listItem);
        }


        return this.Ok(userListItems);
    }

    [HttpPost("changeRole")]
    [Authorize(Roles = $"{RoleHelper.Admin}")]
    public async Task ChangeRole(Guid userId,RoleType newRole)
    {
        
       var user = await this.userManager.FindByIdAsync(userId.ToString());
       await this.userManager.RemoveFromRolesAsync(user,  new List<string> { RoleHelper.Admin, RoleHelper.Teacher, RoleHelper.User });
       var newRoles = new List<string> {RoleHelper.User};
       if (newRole is RoleType.Admin)
       {
           newRoles.Add(RoleHelper.Admin);
       }
       if (newRole is RoleType.Teacher or RoleType.Admin)
       {
           newRoles.Add(RoleHelper.Teacher);
       }
       
       await this.userManager.AddToRolesAsync(user, newRoles);

       
    }
}  