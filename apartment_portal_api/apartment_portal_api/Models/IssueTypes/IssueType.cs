using apartment_portal_api.Abstractions;

namespace apartment_portal_api.Models.IssueTypes;

public class IssueType : Entity
{
    public IssueType(int id, string name) : base(id)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
}