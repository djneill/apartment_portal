using apartment_portal_api.Models.Issues;
using System;
using System.Collections.Generic;

namespace apartment_portal_api.Models.IssueTypes;

public partial class IssueType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    // Linking properties for EF Core
    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
