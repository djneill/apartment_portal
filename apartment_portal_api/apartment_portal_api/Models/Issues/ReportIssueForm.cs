using apartment_portal_api.Models.IssueTypes;
using System.ComponentModel.DataAnnotations;

namespace apartment_portal_api.Models.Issues;

public class ReportIssueForm
{
    [Required(ErrorMessage = "You must select an issue type")]
    public int IssueTypeId { get; set; }
    
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }
}