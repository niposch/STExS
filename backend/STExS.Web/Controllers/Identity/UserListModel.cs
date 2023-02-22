using Application.Helper.Roles;

namespace STExS.Controllers.Identity;

public class UserListModel
{
    public string FirstName { get; set; }= string.Empty;
    public string LastName { get; set; } = string.Empty;
    public RoleType HighestRoleType { get; set; }= RoleType.User;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; } = false;
    
    public string MatrikelNumber { get; set; } = string.Empty;
    
    public Guid UserId { get; set; }
}