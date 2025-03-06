namespace apartment_portal_api.DTOs
{
    public class UnitPatchRequestDTO
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public decimal? Price { get; set; }
        public int? StatusId { get; set; }
    }
}