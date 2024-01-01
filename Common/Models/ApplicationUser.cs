using Microsoft.AspNetCore.Identity;

namespace Lab4.Models;

public class ApplicationUser : IdentityUser
{
    public string? Address { get; set; }
    public ICollection<Orders> Orders { get; set; }

}