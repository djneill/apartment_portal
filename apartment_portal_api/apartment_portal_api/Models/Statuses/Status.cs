using apartment_portal_api.Abstractions;

namespace apartment_portal_api.Models.Statuses;

public sealed class Status : Entity
{
    public Status(
        int id,
        string name)
        : base(id)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
}