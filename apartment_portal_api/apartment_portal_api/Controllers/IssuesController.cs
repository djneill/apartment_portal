using System.Security.Claims;
using apartment_portal_api.Abstractions;
using apartment_portal_api.Services;
using apartment_portal_api.DTOs;
using apartment_portal_api.Models.Issues;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
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
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim is null)
            {
                return Unauthorized();
            }
        
            bool isAdmin = User.IsInRole("Admin");
            string reqUserId = userId.ToString();
            if (!isAdmin && userClaim.Value != reqUserId)
            {
                return Forbid();
            }

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
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userClaim is null)
            {
                return Unauthorized();
            }
                
            int userId = int.Parse(userClaim.Value);
            if (userId == 0)
            {
                return Unauthorized("User ID is missing.");
            }
            
            var issueType = await _unitOfWork.IssueTypeRepository.GetAsync(report.IssueTypeId);
            if (issueType is null)
            {
                return BadRequest("Invalid issue type.");
            }

            var newIssue = new Issue
            {
                UserId = userId,
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