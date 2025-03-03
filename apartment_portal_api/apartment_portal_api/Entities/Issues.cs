using System;

namespace apartment_portal_api.Entities;

public class Issues
{
    public int Id { get; set; }
    public required int UnitUsersId { get; set; }
    public required int IssueTypeId { get; set; }
}
