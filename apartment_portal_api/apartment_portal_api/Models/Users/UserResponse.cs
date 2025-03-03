using apartment_portal_api.Models.Statuses;

namespace apartment_portal_api.Models.Users;

public record UserResponse(int Id, string FirstName, string LastName, DateTime DateOfBirth, Status Status);