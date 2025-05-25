using System.ComponentModel.DataAnnotations;

namespace tehnologiinet.Models;

public class UserDto
{
    [Required(ErrorMessage = "Prenumele este obligatoriu")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Numele este obligatoriu")]
    public string LastName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Emailul nu este valid!")]
    public string Email { get; set; }
}