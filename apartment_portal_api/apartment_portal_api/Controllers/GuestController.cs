using System.Security.Claims;
using apartment_portal_api.Abstractions;
using apartment_portal_api.DTOs;
using apartment_portal_api.Models;
using apartment_portal_api.Models.Guests;
using apartment_portal_api.Models.Users;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
        var guest = await _unitOfWork.GuestRepository.GetAsync(id);
        if (guest == null)
            return NotFound();

        var guestDTO = _mapper.Map<GuestDTO>(guest);
        return Ok(guestDTO);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GuestDTO>>> GetGuests(
        [FromQuery] int? userId,
        [FromQuery] bool? active
    )
    {
        var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (loggedInUserId == null)
        {
            return Unauthorized();
        }

        int.TryParse(loggedInUserId, out int loggedInUserIdInt);
        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userId.HasValue && userId != loggedInUserIdInt)
        {
            return Forbid();
        }

        var guests = await _unitOfWork.GuestRepository.GetAsync(g =>
            (isAdmin || g.UserId == loggedInUserIdInt)
            && (!userId.HasValue || g.UserId == userId)
            && (
                !active.HasValue
                || (active.Value && g.Expiration > DateTime.UtcNow)
                || (!active.Value && g.Expiration <= DateTime.UtcNow)
            )
        );

        if (!guests.Any())
            return NotFound(new { message = "No guests found." });

        var guestDTOs = _mapper.Map<IEnumerable<GuestDTO>>(guests);
        return Ok(new { success = true, data = guestDTOs });
    }

    [HttpPost("register-guest")]
    public async Task<ActionResult<GuestDTO>> CreateGuest(GuestCreateDTO request)
    {
        var newGuest = _mapper.Map<Guest>(request);
        newGuest.CreatedOn = DateTime.UtcNow;

        await _unitOfWork.GuestRepository.AddAsync(newGuest);
        await _unitOfWork.SaveAsync();

        var guestDTO = _mapper.Map<GuestDTO>(newGuest);

        return CreatedAtAction(nameof(GetGuestById), new { id = newGuest.Id }, guestDTO);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<GuestDTO>> EditGuest(int id, GuestPatchDTO patchData)
    {
        if (id != patchData.Id)
            return BadRequest();

        var guestToPatch = await _unitOfWork.GuestRepository.GetAsync(id);
        if (guestToPatch is null)
            return NotFound();

        _mapper.Map(patchData, guestToPatch);
        await _unitOfWork.SaveAsync();

        var editedGuestDTO = _mapper.Map<GuestDTO>(guestToPatch);
        return Ok(editedGuestDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GuestDTO>> UpdateGuest(int id, GuestDTO putData)
    {
        if (id != putData.Id)
            return BadRequest();

        var guestToUpdate = await _unitOfWork.GuestRepository.GetAsync(id);
        if (guestToUpdate is null)
            return NotFound();

        _mapper.Map(putData, guestToUpdate);
        await _unitOfWork.SaveAsync();

        var updatedGuestDTO = _mapper.Map<GuestDTO>(guestToUpdate);
        return Ok(guestToUpdate);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<GuestDTO>> DeleteGuest(int id)
    {
        var guestToDelete = await _unitOfWork.GuestRepository.GetAsync(id);
        if (guestToDelete is null)
            return NotFound();

        _unitOfWork.GuestRepository.Delete(guestToDelete);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("{id:int}/parking-permits")]
    public async Task<ActionResult<ParkingPermitDTO>> GetGuestParkingPermits(int id)
    {
        var guests = await _unitOfWork.GuestRepository.GetAsync(
            p => p.Id == id,
            includeProperties: nameof(Guest.ParkingPermits)
        );
        var guest = guests.FirstOrDefault();
        if (guest is null)
            return NotFound();

        var permitDTOs = _mapper.Map<IEnumerable<ParkingPermitDTO>>(guest.ParkingPermits);

        return Ok(permitDTOs);
    }

    [HttpPatch("{id:int}/parking-permits/{permitId:int}")]
    public async Task<ActionResult<ParkingPermitDTO>> EditGuestParkingPermits(
        int id,
        ParkingPermitPatchDTO patchData
    )
    {
        if (id != patchData.Id)
            return BadRequest();

        var parkingPermitToPatch = await _unitOfWork.ParkingPermitRepository.GetAsync(id);
        if (parkingPermitToPatch is null)
            return NotFound();

        _mapper.Map(patchData, parkingPermitToPatch);
        await _unitOfWork.SaveAsync();

        var editedParkingPermitDTO = _mapper.Map<ParkingPermitDTO>(parkingPermitToPatch);
        return Ok(editedParkingPermitDTO);
    }

    [HttpDelete("{id:int}/parking-permits/{permitId:int}")]
    public async Task<ActionResult<ParkingPermitDTO>> DeleteGuestParkingPermits(int id)
    {
        var parkingPermitToDelete = await _unitOfWork.ParkingPermitRepository.GetAsync(id);
        if (parkingPermitToDelete is null)
            return NotFound();

        _unitOfWork.ParkingPermitRepository.Delete(parkingPermitToDelete);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
