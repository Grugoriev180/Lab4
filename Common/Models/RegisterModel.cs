using System.ComponentModel.DataAnnotations;

namespace Lab4.Models;

public class RegisterModel
{
    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Email address is required")]
    public string EmailAddress { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "Password not match")]
    public string ConfirmPassword { get; set; }
}