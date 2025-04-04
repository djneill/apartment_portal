﻿using apartment_portal_api.Models.Users;

namespace apartment_portal_api.Models.Units;

public record UnitResponse(int Id, int Number, int Price, ICollection<UserResponse> Tenants, Models.Statuses.Status Status);