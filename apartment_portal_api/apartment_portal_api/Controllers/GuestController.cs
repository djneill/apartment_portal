using apartment_portal_api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;
[ApiController]
[Route("[controller]")]
public class GuestController : ControllerBase
{
    
    Random random = new();
    private List<Guest> _guests = new();

        for (int i = 1; i <= 8; i++)
    {
        guests.Add(new Guest
        {
            Id = i,
            UserId = random.Next(1000, 9999),
            FirstName = $"Guest{i}",
            LastName = "Smith",
            Email = $"guest{i}@example.com",
            PhoneNumber = $"+1-555-{random.Next(1000, 9999)}",
            AccessCode = random.Next(100000, 999999).ToString(),
            Expiration = DateTime.UtcNow.AddHours(5), 
            CreatedOn = DateTime.UtcNow 
        })
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<Guest> GetGuestById(int id)
    {
        var guest = _guests.FirstOrDefault(x => x.Id == id);

        if (guest is not null) return Ok(guest);

        return NotFound();
    }

    [HttpGet("/Guests")]
    public ActionResult<ICollection<Guest>> GetGuests()
    {
        
        return Ok(_guests)
            
    }
    
}