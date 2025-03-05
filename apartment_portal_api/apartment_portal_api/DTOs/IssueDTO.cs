namespace apartment_portal_api.DTOs
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int IssueTypeID { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; } = "Open"; // Default to "Open"
        public required DateTime ReportedDate { get; set; } = DateTime.UtcNow;
    }
}