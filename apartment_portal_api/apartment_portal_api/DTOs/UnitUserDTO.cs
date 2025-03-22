namespace apartment_portal_api.DTOs;

public class UnitUserDTO
{
    public int UserId { get; set; }
    public int UnitId { get; set; }
    public bool IsPrimary { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
}
