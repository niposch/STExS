using Application.Helper.Roles;
using Common.Models.Authentication;

namespace STExS.Controllers.Identity;

public class UserDetailsModel
{
    public ApplicationUser User { get; set; }
    public List<RoleType> Roles { get; set; }
}