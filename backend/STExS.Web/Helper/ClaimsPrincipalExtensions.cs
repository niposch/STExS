using System.Security.Claims;

namespace STExS.Helper;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userId ?? throw new Exception());
    }
    
    public static bool IsInAllRoles(this ClaimsPrincipal principal, params string[] roles)
    {
        return roles.All(role => principal.IsInRole(role));
    }
    
    public static bool IsInAnyRoles(this ClaimsPrincipal principal, params string[] roles)
    {
        return roles.Any(role => principal.IsInRole(role));
    }
}