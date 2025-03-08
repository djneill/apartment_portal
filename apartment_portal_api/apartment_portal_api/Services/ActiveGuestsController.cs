using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using apartment_portal_api.DTOs;
using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.Users;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Services;

[ApiController]
[Route("api/active-guests")]
public class ActiveGuestsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public ActiveGuestsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult> GetActiveGuestsByUser(int userId)
    {
        var guests = await _unitOfWork.GuestRepository.GetAsync(
            g => g.UserId == userId && g.Expiration > DateTime.UtcNow);
            //include: query => query.Include(g => g.ApplicationUser) - that might be useful for admin's Manage Specific Tenant view 
        
        if (guests is null || !guests.Any()) return NotFound(new { message = "No active guests found for the user." });
        
        var activeGuests = _mapper.Map<IEnumerable<GuestDTO>>(guests);
        return Ok(new { success = true, data = activeGuests });
    }
        
}