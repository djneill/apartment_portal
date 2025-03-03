using apartment_portal_api.Data;
using apartment_portal_api.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Controllers
{
    [Route("[controller]")] // /users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(PostgresContext context, UserManager<ApplicationUser> userManager)
        {
           _context = context;
           _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")] // /users/12
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { message = $"ApplicationUser with ID {id} not found" });
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create(RegistrationRequest request)
        {
            ApplicationUser newUser = new ApplicationUser();
            {
                newUser.UserName = request.Email;
                newUser.Email = request.Email;
                newUser.FirstName = request.FirstName;
                newUser.LastName = request.LastName;
                newUser.PhoneNumber = request.PhoneNumber;
                newUser.DateOfBirth = request.DateOfBirth;
                newUser.StatusId = request.StatusId;
                newUser.CreatedBy = request.CreatedBy;
                newUser.ModifiedBy = request.ModifiedBy;
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded) return BadRequest();

            return Ok(result);
        }
    }
}
