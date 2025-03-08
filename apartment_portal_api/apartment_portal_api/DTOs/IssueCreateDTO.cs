namespace apartment_portal_api.DTOs
{
    public class IssueCreateDTO
    {
        public required int UserId { get; set; }
        public required int IssueTypeID { get; set; }
        public required string Description { get; set; }
    }
}