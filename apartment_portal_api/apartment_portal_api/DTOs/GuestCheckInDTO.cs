namespace apartment_portal_api.DTOs
{
    public class GuestCheckInDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int UnitId { get; set; }
    }
}