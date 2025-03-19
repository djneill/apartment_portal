namespace apartment_portal_api.DTOs
{
    public class UnitPutRequestDTO
    {
        public int Id { get; set; }
        public int UnitNumber { get; set; }
        public int Price { get; set; }
        public int StatusId { get; set; }
    }
}