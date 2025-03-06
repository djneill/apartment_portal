public class GuestCreateDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string AccessCode { get; set; }
    public DateTime Expiration { get; set; }
}