using apartment_portal_api.Abstractions;
using apartment_portal_api.DTOs;
using apartment_portal_api.Models.UnitUsers;
using apartment_portal_api.Models.Users;
using apartment_portal_api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace apartment_portal_api.Controllers;

[Route("[controller]")] // /users
[ApiController]
[Authorize]
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

    [HttpGet("CurrentUser")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim is null)
            return Unauthorized();

        if (!int.TryParse(userClaim.Value, out int userId))
            return Unauthorized();

        var user = await _unitOfWork.UserRepository.GetAsync(userId);
        var response = _mapper.Map<GetUsersResponse>(user);
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.GetUsers();

        ICollection<ApplicationUser> tenants = new List<ApplicationUser>();
        foreach (var user in users)
        {
            bool isTenant = await _userManager.IsInRoleAsync(user, "Tenant");
            if (isTenant)
                tenants.Add(user);
        }

        var response = _mapper.Map<ICollection<GetUsersResponse>>(tenants);
        return Ok(response);
    }

    [HttpGet("{id}")] // /users/12
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        // Enforce that non-admins can only access their own record.
        var authResult = IsUserOrAdmin(id);
        if (authResult is not null)
            return authResult;

        var user = await _unitOfWork.UserRepository.GetAsync(
            u => u.Id == id,
            $"{nameof(ApplicationUser.UnitUserUsers)}.{nameof(UnitUser.Unit)}"
        );
        var userObj = user.FirstOrDefault();

        if (userObj == null)
        {
            return NotFound(new { message = $"User with ID {id} not found" });
        }

        var unitUser = userObj.UnitUserUsers.FirstOrDefault();
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
            Unit = unitRes
        });
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] RegistrationForm request)
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim is null)
            return Unauthorized();

        // Since this endpoint is restricted to Admins, we assume the caller is an admin.
        int adminId = int.Parse(userClaim.Value);

        if (!EmailValidator.ValidateEmail(request.Email))
            return BadRequest(new { message = "Invalid email format." });

        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return BadRequest(new { message = "A user with this email already exists." });

        var unit = (await _unitOfWork.UnitRepository.GetAsync(u => u.Number == request.UnitNumber)).FirstOrDefault();
        if (unit is null)
            return BadRequest(new { message = $"Unit with number {request.UnitNumber} does not exist." });

        var newUser = _mapper.Map<ApplicationUser>(request);
        newUser.UserName = request.Email;
        newUser.CreatedOn = DateTime.UtcNow;
        newUser.CreatedBy = adminId;
        newUser.ModifiedOn = DateTime.UtcNow;
        newUser.ModifiedBy = adminId;
        newUser.StatusId = 1;

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(newUser, "Tenant");

        var unitUserDto = new UnitUserDTO
        {
            UserId = newUser.Id,
            UnitId = unit.Id,
            CreatedBy = adminId,
            ModifiedBy = adminId
        };

        var unitUser = _mapper.Map<UnitUser>(unitUserDto);
        await _unitOfWork.UnitUserRepository.AddAsync(unitUser);
        await _unitOfWork.SaveAsync();

        return Ok(new { message = "User created successfully!", userId = newUser.Id });
    }

    [HttpGet("roles")]
    [Authorize]
    public async Task<ActionResult<ICollection<string>>> GetRoles()
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim is null)
            return Unauthorized();

        if (!int.TryParse(userClaim.Value, out int userId))
            return Unauthorized();

        var user = await _unitOfWork.UserRepository.GetAsync(userId);
        if (user is null)
            return BadRequest();

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(roles);
    }

    private ActionResult? IsUserOrAdmin(int requestUserId)
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim is null)
        {
            return Unauthorized();
        }

        bool isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userClaim.Value != requestUserId.ToString())
        {
            return Forbid();
        }

        return null;
    }
}