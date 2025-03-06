using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Guests;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
public class GuestController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public GuestController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Guest>> GetGuestById(int id)
    {
        var guest = await _unitOfWork.GuestRepository.GetAsync(id);
        if (guest is not null) return Ok(guest);
        return NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Guest>>> GetGuests()
    {
        var guests = await _unitOfWork.GuestRepository.GetAsync();
        return Ok(guests);
    }

    [HttpPost("register_guest")]
    public async Task<ActionResult> Create(GuestPostRequest request)
    {
        Guest newGuest = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            AccessCode = request.AccessCode,
            Expiration = request.Expiration,
            CreatedOn = DateTime.UtcNow
        };

        await _unitOfWork.GuestRepository.AddAsync(newGuest);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetGuestById), new { id = newGuest.Id }, newGuest);
    }
}