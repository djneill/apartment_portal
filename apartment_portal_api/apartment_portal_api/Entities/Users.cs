using System;

namespace apartment_portal_api.Entities;

public class Users
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required int StatusId { get; set; }
    public required DateTime CreatedOn { get; set; }
    public required int CreatedBy { get; set; }
    public required DateTime ModifiedOn { get; set; }
    public required int ModifiedBy { get; set; }
}
