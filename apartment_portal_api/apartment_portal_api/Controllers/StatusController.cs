using apartment_portal_api.Data;
using apartment_portal_api.Models.Statuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Controllers;


[Route("[controller]"), ApiController, Authorize]
public class StatusController : ControllerBase
{
    private PostgresContext _context;

    public StatusController(PostgresContext context)
    {
        _context = context;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Status>> GetById(int id)
    {
        var status = await _context.Statuses.FirstOrDefaultAsync(x => x.Id == id);
        if (status is null) return NotFound(status);

        return Ok(status);
    }

    [HttpGet("/Statuses")]
    public async Task<ActionResult<ICollection<Status>>> Get()
    {
        return Ok(await _context.Statuses.ToListAsync());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Status status)
    {
        if (id != status.Id) return BadRequest();

        var dbStatus = await _context.Statuses.FirstOrDefaultAsync(x => x.Id == id);
        if (dbStatus is null) return BadRequest();

        dbStatus.Name = status.Name;

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Create(StatusPostRequest postData)
    {
        Status newUnit = new Status{Name = postData.Name};
        await _context.Statuses.AddAsync(newUnit);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = newUnit.Id },
            newUnit
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var statusToDelete = await _context.Statuses.FirstOrDefaultAsync(unit => unit.Id == id);
        if (statusToDelete is null) return BadRequest();

        _context.Statuses.Remove(statusToDelete);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, StatusPatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var statusToPatch = await _context.Statuses.FirstOrDefaultAsync(status => status.Id == id);
        if (statusToPatch is null) return BadRequest();

        statusToPatch.Name = patchData.Name ?? statusToPatch.Name;

        await _context.SaveChangesAsync();

        return Ok();
    }
}
