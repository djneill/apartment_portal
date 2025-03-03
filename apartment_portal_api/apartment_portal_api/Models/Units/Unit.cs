using apartment_portal_api.Models.Packages;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.UnitUsers;
using System;
using System.Collections.Generic;

namespace apartment_portal_api.Models;

public partial class Unit
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int Price { get; set; }

    public int StatusId { get; set; }

    // Linking properties for EF Core
    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<UnitUser> UnitUsers { get; set; } = new List<UnitUser>();
}
