namespace apartment_portal_api.Models.Guests;

public record GuestPutRequest(int Id, string FirstName, string LastName, string PhoneNumber, int DurationInHours);