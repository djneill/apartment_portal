namespace apartment_portal_api.DTOs
{
    public class IssueUpdateDTO
    {
        public required int Id { get; set; }
        public required string Status { get; set; } = "Open";
    }
}