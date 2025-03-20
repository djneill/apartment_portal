namespace apartment_portal_api.Models.LeaseAgreements;

public record LeaseAgreementPostRequest(DateOnly StartDate, DateOnly EndDate, string Link, int UserId);