namespace apartment_portal_api.Models.Guests;

public record GuestPatchRequest(
    int? UserId,
    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber,
    string? AccessCode,
    DateTime? Expiration)