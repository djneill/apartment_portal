namespace apartment_portal_api.DTOs;

public class UnitUserDTO
{
    public int UserId { get; set; }
    public int UnitId { get; set; }
    public bool IsPrimary { get; set; } = true; 
    public string LeaseAgreement { get; set; } = "Active"; 
    public DateTime LeaseExpiration { get; set; } = DateTime.UtcNow.AddYears(1);
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
}
