using Application.Interfaces;

namespace Application.Helper.Roles;

public class RoleHelper : IRoleHelper
{
    public const string Admin = "admin";
    public const string Teacher = "teacher";
    public const string User = "user";

    private readonly Dictionary<RoleType, string> typeToString = new Dictionary<RoleType, string>
    {
        { RoleType.Admin, Admin },
        { RoleType.Teacher, Teacher },
        { RoleType.User, User }
    };

    private readonly Dictionary<string, RoleType> stringToType = new Dictionary<string, RoleType>
    {
        { Admin, RoleType.Admin },
        { Teacher, RoleType.Teacher },
        { User, RoleType.User }
    };

    public string ToString(RoleType roleType)
    {
        return this.typeToString[roleType];
    }

    public RoleType GetHighestRole(List<RoleType> roleTypes)
    {
        if (roleTypes.Contains(RoleType.Admin))
        {
            return RoleType.Admin;
        }

        if (roleTypes.Contains(RoleType.Teacher))
        {
            return RoleType.Teacher;
        }
        return RoleType.User;
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