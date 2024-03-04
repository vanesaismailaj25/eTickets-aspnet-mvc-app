using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels;

public class RegisterVM
{
    [Required(ErrorMessage = "Full name is required!")]
    [Display(Name = "Full name")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email address is required!")]
    [Display(Name = "Email address")]
    public string EmailAddress { get; set; }
    
    [Required(ErrorMessage = "Password is required!")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required!")]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match!")]
    public string ConfirmPassword { get; set; }
}
