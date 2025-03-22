using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Guests;
using apartment_portal_api.Models.ParkingPermits;
using apartment_portal_api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        var isUserOrAdmin = IsUserOrAdmin(guest.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

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
            (isAdmin || g.UserId == loggedInUserIdInt) &&
             (!userId.HasValue || g.UserId == userId)
            && (
                !active.HasValue
                || (active.Value && g.Expiration > DateTime.UtcNow)
                || (!active.Value && g.Expiration <= DateTime.UtcNow)
            )
        );

        if (!guests.Any())
            return NotFound(new { message = "No guests found." });

        var guestDTOs = _mapper.Map<IEnumerable<GuestDTO>>(guests);
        return Ok(guestDTOs);
    }

    [HttpPost("register-guest")]
    public async Task<ActionResult<GuestDTO>> CreateGuest(GuestPostRequest request)
    {
        var isUserOrAdmin = IsUserOrAdmin(request.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

        Guest newGuest = _mapper.Map<Guest>(request);
        newGuest.AccessCode = AccessCodeGenerator.GenerateAccessCode();
        
        if (request.DurationInHours > 0)
        {
            newGuest.Expiration = DateTime.UtcNow.AddHours(request.DurationInHours);
        }
        else
        {
            return BadRequest("Invalid duration provided.");
        }

        if (request.ParkingPermit is not null)
        {
            ParkingPermit permit = _mapper.Map<ParkingPermit>(request.ParkingPermit);
            newGuest.ParkingPermits.Add(permit);
        }

        await _unitOfWork.GuestRepository.AddAsync(newGuest);
        await _unitOfWork.SaveAsync();
        return Created();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> EditGuest(int id, GuestPatchDTO patchData)
    {
        if (id != patchData.Id)
            return BadRequest();

        var guestToPatch = await _unitOfWork.GuestRepository.GetAsync(id);
        if (guestToPatch is null)
            return NotFound();

        var isUserOrAdmin = IsUserOrAdmin(guestToPatch.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

        guestToPatch.FirstName = patchData.FirstName ?? guestToPatch.FirstName;
        guestToPatch.LastName = patchData.LastName ?? guestToPatch.LastName;
        guestToPatch.PhoneNumber = patchData.PhoneNumber ?? guestToPatch.PhoneNumber;
        guestToPatch.Expiration = CalculateExpiration(patchData.DurationInHours, guestToPatch.Expiration);
        await _unitOfWork.SaveAsync();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateGuest(int id, GuestPutRequest putData)
    {
        if (id != putData.Id)
            return BadRequest();

        var guestToUpdate = await _unitOfWork.GuestRepository.GetAsync(id);
        if (guestToUpdate is null)
            return NotFound();

        var isUserOrAdmin = IsUserOrAdmin(guestToUpdate.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;


        guestToUpdate.FirstName = putData.FirstName;
        guestToUpdate.LastName = putData.LastName;
        guestToUpdate.PhoneNumber = putData.PhoneNumber;
        guestToUpdate.Expiration = CalculateExpiration(putData.DurationInHours, guestToUpdate.Expiration);
        await _unitOfWork.SaveAsync();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteGuest(int id)
    {
        var guest = await _unitOfWork.GuestRepository.GetAsync(g => g.Id == id, nameof(Guest.ParkingPermits));
        var guestToDelete = guest.FirstOrDefault();
        if (guestToDelete is null)
            return NotFound();

        var isUserOrAdmin = IsUserOrAdmin(guestToDelete.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

        foreach (ParkingPermit parkingPermit in guestToDelete.ParkingPermits)
        {
            _unitOfWork.ParkingPermitRepository.Delete(parkingPermit);
        }
        _unitOfWork.GuestRepository.Delete(guestToDelete);
        await _unitOfWork.SaveAsync();
        return Ok();
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

        var isUserOrAdmin = IsUserOrAdmin(guest.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

        var permitDTOs = _mapper.Map<IEnumerable<ParkingPermitDTO>>(guest.ParkingPermits);

        return Ok(permitDTOs);
    }

    [HttpPatch("{id:int}/parking-permits/{permitId:int}")]
    public async Task<ActionResult> EditGuestParkingPermits(
        int id,
        int permitId,
        ParkingPermitPatchDTO patchData
    )
    {
        if (permitId != patchData.Id)
            return BadRequest();

        var parkingPermits = await _unitOfWork.ParkingPermitRepository.GetAsync(pp => pp.Id == id, nameof(ParkingPermit.Guest));
        var parkingPermitToPatch = parkingPermits.FirstOrDefault();
        if (parkingPermitToPatch is null)
            return NotFound();

        var isUserOrAdmin = IsUserOrAdmin(parkingPermitToPatch.Guest.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

        parkingPermitToPatch.VehicleMake = patchData.VehicleMake ?? parkingPermitToPatch.VehicleMake;
        parkingPermitToPatch.VehicleModel = patchData.VehicleModel ?? parkingPermitToPatch.VehicleModel;
        parkingPermitToPatch.LicensePlate = patchData.LicensePlate ?? parkingPermitToPatch.LicensePlate;
        parkingPermitToPatch.LicensePlateState = patchData.LicensePlateState ?? parkingPermitToPatch.LicensePlateState;

        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpDelete("{guestId:int}/parking-permits/{permitId:int}")]
    public async Task<ActionResult> DeleteGuestParkingPermits(int guestId, int permitId)
    {
        var parkingPermit = await _unitOfWork.ParkingPermitRepository
            .GetAsync(p => p.Id == permitId && p.GuestId == guestId, nameof(ParkingPermit.Guest));
        var parkingPermitToDelete = parkingPermit.FirstOrDefault();

        if (parkingPermitToDelete is null)
            return NotFound();

        var isUserOrAdmin = IsUserOrAdmin(parkingPermitToDelete.Guest.UserId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;

        _unitOfWork.ParkingPermitRepository.Delete(parkingPermitToDelete);
        await _unitOfWork.SaveAsync();

        return Ok();
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

    private DateTime CalculateExpiration(int? durationInHours, DateTime currentExpiration)
    {
        if (durationInHours is null)
            return currentExpiration;
        
        DateTime baseTime = currentExpiration > DateTime.UtcNow ? currentExpiration : DateTime.UtcNow;

        DateTime newExpiration = baseTime.AddHours((int)durationInHours);

        return newExpiration;
    }
}
