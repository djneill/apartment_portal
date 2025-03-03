

namespace apartment_portal_api.Models.Users;

public record UserResponse(int Id, string FirstName, string LastName, DateTime DateOfBirth, Statuses.Status Status);