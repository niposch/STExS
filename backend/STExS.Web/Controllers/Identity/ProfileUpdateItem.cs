namespace STExS.Controllers.Identity;

public class ProfileUpdateItem
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? Email { get; set; } // leave null if it wasn't changed
    
    public string UserName { get; set; }
    
    public string MatrikelNumber { get; set; }
    
    public string PhoneNumber { get; set; }
}