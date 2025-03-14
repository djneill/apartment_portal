using apartment_portal_api.Models.UnitUsers;
using System.ComponentModel.DataAnnotations;

namespace apartment_portal_api.Models.Users;
public class RegistrationForm
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Date of Birth is required.")]
    public DateOnly DateOfBirth { get; set; }
    [Required(ErrorMessage = "Unit is required.")]
    public int UnitNumber { get; set; }
}