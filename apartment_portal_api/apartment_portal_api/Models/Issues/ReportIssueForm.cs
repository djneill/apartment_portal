using apartment_portal_api.Models.IssueTypes;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace apartment_portal_api.Models.Issues;

public class ReportIssueForm
{
    // remove UserId when auth is reinstated 
    public int UserId {get; set;}

    [Required(ErrorMessage = "You must select an issue type")]
    public int IssueTypeId { get; set; }
    
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }
}