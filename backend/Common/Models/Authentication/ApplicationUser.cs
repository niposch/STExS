using Common.Models.ExerciseSystem;
using Common.Models.HelperInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Common.Models.Authentication;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MatrikelNumber { get; set; } = string.Empty;
    
    public List<ModuleParticipation> ModuleParticipations { get; set; } = new List<ModuleParticipation>();
}

public class ApplicationRole : IdentityRole<Guid>
{
}