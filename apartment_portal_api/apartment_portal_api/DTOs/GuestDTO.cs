namespace apartment_portal_api.DTOs
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime Expiration { get; set; }
        public int AccessCode { get; set; }
    }
}