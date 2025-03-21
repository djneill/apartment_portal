using apartment_portal_api.Abstractions;
using apartment_portal_api.DTOs;
using apartment_portal_api.Models.LeaseAgreements;
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
public class UsersController(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    IMapper mapper)
    : ControllerBase
{
    [HttpGet("CurrentUser")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaim is null)
        {
            return Unauthorized();
        }

        int.TryParse(userClaim.Value, out int userId);

        var user = await unitOfWork.UserRepository.GetAsync(userId);

        var response = mapper.Map<GetUsersResponse>(user);

        return Ok(response);
    }


    [HttpGet]
    // Uncomment next line to add auth
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await unitOfWork.UserRepository.GetUsers();

        ICollection<ApplicationUser> tenants = [];

        foreach (var user in users)
        {
            bool isTenant = await userManager.IsInRoleAsync(user, "Tenant");
            if (isTenant) tenants.Add(user);
        }

        var response = mapper.Map<ICollection<GetUsersResponse>>(tenants);

        return Ok(response);
    }

    [HttpGet("{id}")] // /users/12
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var user = await unitOfWork.UserRepository.GetAsync(
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

    [HttpPost("register"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] RegistrationForm request)
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaim is null)
        {
            return Unauthorized();
        }

        int adminId = int.Parse(userClaim.Value);

        if (!EmailValidator.ValidateEmail(request.Email))
        {
            return BadRequest(new { message = "Invalid email format." });
        }

        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return BadRequest(new { message = "A user with this email already exists." });
        }

        var unit = (await unitOfWork.UnitRepository.GetAsync(u => u.Number == request.UnitNumber)).FirstOrDefault();
        if (unit is null)
        {
            return BadRequest(new { message = $"Unit with number {request.UnitNumber} does not exist." });
        }

        var newUser = mapper.Map<ApplicationUser>(request);
        newUser.UserName = request.Email;
        newUser.CreatedBy = adminId;
        newUser.ModifiedBy = adminId;
        newUser.StatusId = 1;

        var result = await userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await userManager.AddToRoleAsync(newUser, "Tenant");

        var unitUserDto = new UnitUserDTO
        {
            UserId = newUser.Id,
            UnitId = unit.Id,
            CreatedBy = adminId,
            ModifiedBy = adminId,
            IsPrimary = request.IsPrimary
        };

        var unitUser = mapper.Map<UnitUser>(unitUserDto);

        LeaseAgreement agreement = new()
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Link = $"leaseagreement.com/{newUser.Id}/{unit.Number}",  // TODO: Update this line when we know what the link is
            LeaseStatusId = 3,
            UnitUser = unitUser
        };

        await unitOfWork.LeaseAgreementRepository.AddAsync(agreement);
        await unitOfWork.SaveAsync();

        return Ok(new { message = "User created successfully!", userId = newUser.Id });
    }

    // Uncomment line below when turning on auth
    [Authorize]
    [HttpGet("roles")]
    public async Task<ActionResult<ICollection<string>>> GetRoles()
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaim is null)
        {
            return Unauthorized();
        }

        int.TryParse(userClaim.Value, out int userId);

        var user = await unitOfWork.UserRepository.GetAsync(userId);

        if (user is null) return BadRequest();

        var roles = await userManager.GetRolesAsync(user);

        return Ok(roles);
    }
}