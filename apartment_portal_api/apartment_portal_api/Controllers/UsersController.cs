using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Users;
using apartment_portal_api.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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
        var user = await _unitOfWork.UserRepository.GetAsync(id);

        if (user == null)
        {
            return NotFound(new { message = $"User with ID {id} not found" });
        }

        var userDTO = _mapper.Map<UserDTO>(user);
        return Ok(userDTO);
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