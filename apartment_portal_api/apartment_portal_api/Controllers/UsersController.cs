using System.Collections;
using System.Security.Claims;
using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Users;
using apartment_portal_api.Models.UnitUsers;
using apartment_portal_api.DTOs;
using apartment_portal_api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

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
    // Uncomment next line to add auth
    // [Authorize(Roles="Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.GetUsers();

        ICollection<ApplicationUser> tenants = [];

        foreach (var user in users)
        {
            bool isTenant = await _userManager.IsInRoleAsync(user, "Tenant");
            if (isTenant) tenants.Add(user);
        }

        var response = _mapper.Map<ICollection<GetUsersResponse>>(tenants);

        return Ok(response);
    }

    [HttpGet("{id}")] // /users/12
    public async Task<IActionResult> GetUserByIdAsync(int id)
    { 
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
            Unit = unitRes
        });
    }

    [HttpPost("register")]
    // Uncomment next line for auth
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] RegistrationForm request)
    {
        //var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        //if (userClaim is null)
        //{
        //    return Unauthorized();
        //}

        //int adminId = int.Parse(userClaim.Value);

        if (!EmailValidator.ValidateEmail(request.Email))
        {
            return BadRequest(new { message = "Invalid email format." });
        }
        
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return BadRequest(new { message = "A user with this email already exists." });
        }
        
        var unit = (await _unitOfWork.UnitRepository.GetAsync(u => u.Number == request.UnitNumber)).FirstOrDefault();
        if (unit is null)
        {
            return BadRequest(new { message = $"Unit with number {request.UnitNumber} does not exist." });
        }
        
        var newUser = _mapper.Map<ApplicationUser>(request);
        newUser.UserName = request.Email;
        newUser.CreatedBy = 4;  // fix when uncommenting auth
        newUser.ModifiedBy = 4;  // fix when uncommenting auth
        newUser.StatusId = 1;
        
        var result = await _userManager.CreateAsync(newUser, request.Password);
    
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(newUser, "Tenant");
        
        var unitUserDto = new UnitUserDTO
        {
            UserId = newUser.Id,
            UnitId = unit.Id,
            CreatedBy = 4,      //fix when uncommenting auth
            ModifiedBy = 4      // fix when uncommenting auth
        };

        var unitUser = _mapper.Map<UnitUser>(unitUserDto);
        await _unitOfWork.UnitUserRepository.AddAsync(unitUser);
        await _unitOfWork.SaveAsync();
            
        return Ok(new { message = "User created successfully!", userId = newUser.Id });
    }

    [HttpGet("{id:int}/expirationCountdown")]
    public async Task<ActionResult> GetLeaseExpiration(int id)
    {
        //bool isAdmin = User.IsInRole("Admin");
        //var loggedInUserIdClaim = User.Claims.FirstOrDefault(claim => claim.Value == id.ToString());
        //if (!isAdmin && loggedInUserIdClaim is null) return Unauthorized();

        var userRes =
            await _unitOfWork.UserRepository
                .GetAsync(u => u.Id == id, nameof(ApplicationUser.UnitUserUsers));

        var user = userRes.FirstOrDefault();
        var unit = user?.UnitUserUsers.FirstOrDefault();

        if (user is null || unit is null)
        {
            return NotFound();
        }

        var timeDifference = unit.LeaseExpiration - DateTime.UtcNow;

        return Ok( new
        {
            ExpirationCountdown = timeDifference
        });
    }

    // Uncomment line below when turning on auth
    // [Authorize]
    [HttpGet("roles")]
    public async Task<ActionResult<ICollection<string>>> GetRoles()
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaim is null)
        {
            return Unauthorized();
        }

        int.TryParse(userClaim.Value, out int userId);

        var user = await _unitOfWork.UserRepository.GetAsync(userId);

        if (user is null) return BadRequest();

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(roles);
    }
}