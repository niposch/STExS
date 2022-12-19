using Application.Interfaces;

namespace Application.Helper.Roles;

public class RoleHelper : IRoleHelper
{
    private readonly Dictionary<RoleType, string> typeToString = new Dictionary<RoleType, string>
    {
        { RoleType.Admin, "admin" },
        { RoleType.Teacher, "teacher" },
        { RoleType.User, "user" }
    };

    private readonly Dictionary<string, RoleType> stringToType = new Dictionary<string, RoleType>
    {
        { "admin", RoleType.Admin },
        { "teacher", RoleType.Teacher },
        { "user", RoleType.User }
    };

    public string ToString(RoleType roleType)
    {
        return this.typeToString[roleType];
    }

    public RoleType? Parse(string role)
    {
        if (!this.stringToType.ContainsKey(role))
        {
            return null;
        }

        return this.stringToType[role];
    }
}

public enum RoleType
{
    User,
    Teacher,
    Admin
}