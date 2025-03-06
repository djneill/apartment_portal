namespace apartment_portal_api.DTOs
{
    public class UnitResponseDTO
    {
        public int Id { get; set; }
        public required string Number { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public required string StatusName { get; set; } // If you need status details
    }
}