using apartment_portal_api.Models.Statuses;

namespace apartment_portal_api.Models.Users;

public class UserResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public StatusResponse Status { get; set; }
}