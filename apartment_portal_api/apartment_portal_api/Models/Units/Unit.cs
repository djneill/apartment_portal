using apartment_portal_api.Abstractions;

namespace apartment_portal_api.Models.Units;

public sealed class Unit: Entity
{
    public Unit(
        int id,
        int number,
        int price,
        int statusId,
        int unitUserId)
        : base(id)
    {
        Id = id;
        Number = number;
        Price = price;
        StatusId = statusId;
        UnitUserId = unitUserId;
    }

    public int Number { get; set; }
    public int Price { get; set; }
    public int StatusId { get; set; }
    public int UnitUserId { get; set; }
}