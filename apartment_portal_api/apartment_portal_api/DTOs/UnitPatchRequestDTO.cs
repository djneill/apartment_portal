namespace apartment_portal_api.DTOs
{
    public class UnitPatchRequestDTO
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public int? Price { get; set; }
        public int? StatusId { get; set; }
    }
}