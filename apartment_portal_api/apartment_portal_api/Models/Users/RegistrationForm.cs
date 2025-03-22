using System.ComponentModel.DataAnnotations;

namespace apartment_portal_api.Models.Users;
public class RegistrationForm
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = default!;

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = default!;

    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; } = default!;

    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "Date of Birth is required.")]
    public DateOnly DateOfBirth { get; set; }

    [Required(ErrorMessage = "Unit is required.")]
    public int UnitNumber { get; set; }

    [Required(ErrorMessage = "IsPrimary is required")]
    public bool IsPrimary { get; set; }

    [Required(ErrorMessage = "Start Date is required.")]
    public DateOnly StartDate { get; set; }

    [Required(ErrorMessage = "End Date is required.")]
    public DateOnly EndDate { get; set; }
}