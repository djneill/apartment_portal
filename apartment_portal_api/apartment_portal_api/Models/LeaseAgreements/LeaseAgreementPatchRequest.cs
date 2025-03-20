namespace apartment_portal_api.Models.LeaseAgreements;

public record LeaseAgreementPatchRequest(int Id, DateOnly? StartDate, DateOnly? EndDate, DateOnly? SignedOn, string? Link, int? StatusId);