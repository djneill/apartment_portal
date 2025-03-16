using apartment_portal_api.Models.Statuses;

namespace apartment_portal_api.Models.Users;

public class GetUsersResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public StatusResponse Status { get; set; } = default!;
    public GetUsersUnitResponse? Unit { get; set; } = default!;
}

public class GetUsersUnitResponse
{
    public int Id { get; init; }
    public int Number { get; init; }
    public int Price { get; init; }
    public StatusResponse Status { get; init; } = default!;
}