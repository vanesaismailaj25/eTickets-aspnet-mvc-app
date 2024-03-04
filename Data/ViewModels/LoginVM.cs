using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels;

public class LoginVM
{
    [Required(ErrorMessage = "Email address is required!")]
    [Display(Name = "Email address")]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Password is required!")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
