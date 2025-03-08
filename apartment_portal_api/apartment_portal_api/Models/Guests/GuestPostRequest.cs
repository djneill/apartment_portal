namespace apartment_portal_api.Models.Guests;

 public record GuestPostRequest(
     int UserId,
     string FirstName,
     string LastName,
     string Email,
     string PhoneNumber,
     string? AccessCode,
     DateTime Expiration,
     DateTime CreatedOn);