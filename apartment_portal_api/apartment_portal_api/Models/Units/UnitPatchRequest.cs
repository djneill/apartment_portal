namespace apartment_portal_api.Models.Units;

public record UnitPatchRequest(int Id, int? Number, int? Price, int? StatusId);
