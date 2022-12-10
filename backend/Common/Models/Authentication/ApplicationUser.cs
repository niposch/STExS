using Common.Models.HelperInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Common.Models.Authentication;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; } = null;

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MatrikelNumber { get; set; } = string.Empty;
}

public class ApplicationRole : IdentityRole<Guid>
{
}