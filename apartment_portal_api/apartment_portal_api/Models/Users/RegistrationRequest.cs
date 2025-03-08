namespace apartment_portal_api.Models.Users;

public record RegistrationRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string PhoneNumber,
    DateOnly DateOfBirth,
    int StatusId,
    int CreatedBy,
    int ModifiedBy);