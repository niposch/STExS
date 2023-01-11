using Application.Helper.Roles;

namespace Application.DTOs.UserAdmin;

public class UserListItem
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string MatrikelNummer { get; set; }
    public string PhoneNumber { get; set; }
    public RoleType CurrentRole { get; set; }
    public Guid UserId { get; set; }
    
}