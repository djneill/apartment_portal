using System.Security.Claims;
using apartment_portal_api.Abstractions;
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
    }
}