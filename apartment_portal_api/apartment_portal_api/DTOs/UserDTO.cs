namespace apartment_portal_api.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int UnitId { get; set; }
    }
}