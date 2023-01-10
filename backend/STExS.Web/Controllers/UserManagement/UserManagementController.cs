using Application.Helper.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STExS.Controllers.UserManagement;

[Authorize(Roles = $"{RoleHelper.Admin}")]
public class UserManagementController
{
    [HttpGet("all")]
    public async Task GetAllUsers()
    {
    }
}