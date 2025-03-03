using apartment_portal_api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IssuesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetIssues()
        {
            var issues = await _context.Issue.ToListAsync();
            return Ok(issues);
        }
    }
}
