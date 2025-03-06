namespace apartment_portal_api.DTOs
{
    public class GuestApprovalDTO
    {
        public required int GuestId { get; set; }
        public required bool IsApproved { get; set; }
    }
}