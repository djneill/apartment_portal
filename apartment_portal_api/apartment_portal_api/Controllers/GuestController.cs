using apartment_portal_api.Abstractions;
using apartment_portal_api.Models;
using apartment_portal_api.Models.Users;
using apartment_portal_api.DTOs;
using apartment_portal_api.Models.Guests;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
public class GuestController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GuestController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GuestDTO>> GetGuestById(int id)
    {
        // Fetch the guest by id
        var guest = await _unitOfWork.GuestRepository.GetAsync(id);

        // If the guest is not found, return NotFound
        if (guest == null)
            return NotFound();

        // Map the Guest entity to GuestDTO for the response
        var guestDTO = _mapper.Map<GuestDTO>(guest);
        return Ok(guestDTO);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GuestDTO>>> GetGuests()
    {
        // Fetch all guests
        var guests = await _unitOfWork.GuestRepository.GetAsync();

        // Map the list of guests to a list of GuestDTOs
        var guestDTOs = _mapper.Map<IEnumerable<GuestDTO>>(guests);
        return Ok(guestDTOs);
    }

    [HttpPost("register_guest")]
    public async Task<ActionResult<GuestDTO>> Create(GuestCreateDTO request)
    {
        // Map the incoming GuestCreateDTO to a Guest entity
        var newGuest = _mapper.Map<Guest>(request);
        newGuest.CreatedOn = DateTime.UtcNow;

        // Add the new guest entity to the repository
        await _unitOfWork.GuestRepository.AddAsync(newGuest);
        await _unitOfWork.SaveAsync();

        // Map the newly created Guest entity to GuestDTO for the response
        var guestDTO = _mapper.Map<GuestDTO>(newGuest);

        // Return the created guest's DTO along with a CreatedAtAction response
        return CreatedAtAction(nameof(GetGuestById), new { id = newGuest.Id }, guestDTO);
    }
}
    // Random random = new();
    // private List<Guest> _guests = new();

    //     for (int i = 1; i <= 8; i++)
    // {
    //     guests.Add(new Guest
    //     {
    //         Id = i,
    //         UserId = random.Next(1000, 9999),
    //         FirstName = $"Guest{i}",
    //         LastName = "Smith",
    //         Email = $"guest{i}@example.com",
    //         PhoneNumber = $"+1-555-{random.Next(1000, 9999)}",
    //         AccessCode = random.Next(100000, 999999).ToString(),
    //         Expiration = DateTime.UtcNow.AddHours(5), 
    //         CreatedOn = DateTime.UtcNow 
    //     })
    // }

    // [HttpGet("{id:int}")]
    // public ActionResult<Guest> GetGuestById(int id)
    // {
    //     var guest = _guests.FirstOrDefault(x => x.Id == id);

    //     if (guest is not null) return Ok(guest);

    //     return NotFound();
    // }

    // [HttpGet("/Guests")]
    // public ActionResult<ICollection<Guest>> GetGuests()
    // {

    //     return Ok(_guests)

    // }

