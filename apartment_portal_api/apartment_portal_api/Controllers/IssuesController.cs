using apartment_portal_api.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[Route("[controller]")]
[ApiController]
public class IssuesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public IssuesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetIssues()
    {
        var issues = await _unitOfWork.IssueRepository.GetAsync();
        return Ok(issues);
    }
}
