using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[Route("[controller]")] // /users
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAsync();
        return Ok(users);
    }

    [HttpGet("{id}")] // /users/12
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(id);

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
