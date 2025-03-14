using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Users;
using apartment_portal_api.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using apartment_portal_api.Models.UnitUsers;

namespace apartment_portal_api.Controllers;

[Route("[controller]")] // /users
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UsersController(
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAsync();
        var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
        return Ok(userDTOs);
    }

    [HttpGet("{id}")] // /users/12
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        // Eagerly load Unit along with UnitUserUsers
        var user = await _unitOfWork.UserRepository.GetAsync(
            u => u.Id == id,
            $"{nameof(ApplicationUser.UnitUserUsers)}.{nameof(UnitUser.Unit)}"
        );

        if (user == null)
        {
            return NotFound(new { message = $"User with ID {id} not found" });
        }

        var userObj = user.FirstOrDefault();

        var unitUser = userObj?.UnitUserUsers?.FirstOrDefault();
        if (unitUser == null)
        {
            return NotFound(new { message = "No unit found for the given user." });
        }

        var unitRes = new UnitDTO()
        {
            Id = unitUser.Unit.Id,
            UnitNumber = unitUser.Unit.Number,
            Price = unitUser.Unit.Price
        };

        return Ok(new
        {
            User = new UserDTO()
            {
                Id = userObj.Id,
                FirstName = userObj.FirstName,
                LastName = userObj.LastName,
                DateOfBirth = userObj.DateOfBirth,
                StatusId = userObj.StatusId
            },
            Unit = unitRes // Returning a single unit since each user has only one
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create(RegistrationRequestDTO request)
    {
        var newUser = _mapper.Map<ApplicationUser>(request);

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok(_mapper.Map<UserDTO>(newUser));
    }
}