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
        public async Task<IActionResult> GetIssues()
        {
            var issues = await _unitOfWork.IssueRepository.GetAsync();
            var issueDTOs = _mapper.Map<IEnumerable<IssueDTO>>(issues);
            return Ok(issueDTOs);
        }
    }
}