using System.Security.Claims;
using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Issues;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace apartment_portal_api.Controllers
{
    [Authorize]
    public class IssuesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IssuesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<IssueResponse>>> GetIssues(int userId = 0, int recordRetrievalCount = 10, int statusId = 0, bool orderByDesc = true)
        {
            // Retrieve the current user's id.
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userClaim))
                return Unauthorized();

            int loggedInUserId = int.Parse(userClaim);
            bool isAdmin = User.IsInRole("Admin");

            // If a userId filter is provided and the caller is not an admin, 
            // ensure that the requested userId matches the logged-in user.
            if (!isAdmin && userId != 0 && userId != loggedInUserId)
                return Forbid();

            // For non-admins with no specified userId, set the filter to the logged-in user's id.
            if (!isAdmin && userId == 0)
                userId = loggedInUserId;

            ICollection<Issue> issues = await _unitOfWork.IssueRepository.GetIssues(userId, recordRetrievalCount, statusId, orderByDesc);
            var response = _mapper.Map<ICollection<IssueResponse>>(issues);
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueById(int id)
        {
            var issue = await _unitOfWork.IssueRepository.GetAsync(id);
            if (issue == null)
            {
                return NotFound(new { message = "Issue not found." });
            }
            // Optionally, if you want to restrict access to the issue by its owner (non-admins):
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userClaim))
            {
                int loggedInUserId = int.Parse(userClaim);
                bool isAdmin = User.IsInRole("Admin");
                if (!isAdmin && issue.UserId != loggedInUserId)
                    return Forbid();
            }
            return Ok(issue);
        }
        
        [HttpGet("types")]
        public async Task<IActionResult> GetIssueTypes()
        {
            var issueTypes = await _unitOfWork.IssueTypeRepository.GetAsync();
            var issueType = issueTypes.Select(i => new { i.Id, i.Name });
            return Ok(issueType);
        }

        [HttpPost("report")]
        public async Task<IActionResult> ReportIssue([FromBody] ReportIssueForm report)
        {
            // Ensure the caller is authenticated.
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userClaim))
                return Unauthorized();

            int loggedInUserId = int.Parse(userClaim);
            bool isAdmin = User.IsInRole("Admin");
            // For non-admins, the report's UserId must match the logged-in user.
            if (!isAdmin && report.UserId != loggedInUserId)
                return Forbid();
            
            var issueType = await _unitOfWork.IssueTypeRepository.GetAsync(report.IssueTypeId);
            if (issueType is null)
            {
                return BadRequest("Invalid issue type.");
            }

            var newIssue = new Issue
            {
                UserId = report.UserId,
                IssueTypeId = report.IssueTypeId,
                Description = report.Description,
                CreatedOn = DateTime.UtcNow,
                StatusId = 1
            };

            await _unitOfWork.IssueRepository.AddAsync(newIssue);
            await _unitOfWork.SaveAsync();
            
            return CreatedAtAction(nameof(GetIssueById), new { id = newIssue.Id }, new { message = "Issue reported successfully!", issueId = newIssue.Id });
        }
    }
}