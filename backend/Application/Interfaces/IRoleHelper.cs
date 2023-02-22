using Application.Helper.Roles;

namespace Application.Interfaces;

public interface IRoleHelper
{
    public string ToString(RoleType roleType);

    public RoleType? Parse(string role);
    public RoleType GetHighestRole(List<RoleType> roleTypes);
}