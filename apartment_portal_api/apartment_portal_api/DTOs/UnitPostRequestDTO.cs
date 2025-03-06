namespace apartment_portal_api.DTOs
{
    public class UnitPostRequestDTO
    {
        public required string Number { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
    }
}