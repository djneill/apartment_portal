namespace apartment_portal_api.DTOs
{
    public class UnitPostRequestDTO
    {
        public required int Number { get; set; }
        public int Price { get; set; }
        public int StatusId { get; set; }
    }
}