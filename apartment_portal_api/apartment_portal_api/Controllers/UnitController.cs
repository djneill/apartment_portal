using apartment_portal_api.Data;
using apartment_portal_api.Models;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.Units;
using apartment_portal_api.Models.UnitUsers;
using apartment_portal_api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase
{
    private PostgresContext _context;
    public UnitController(PostgresContext context)
    {
        _context = context;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UnitResponse>> GetById(int id)
    {
        var unit = await _context.Units.FirstOrDefaultAsync(x => x.Id == id);
        if (unit is null) return BadRequest();
        

        return Ok(unit);
    }

    [HttpGet("/Units")]
    public async Task<ActionResult<ICollection<Unit>>> Get()
    {
        return Ok(await _context.Units.ToListAsync());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Unit unit)
    {
        if (id != unit.Id) return BadRequest();

        var dbUnit = await _context.Units.FirstOrDefaultAsync(x => x.Id == id);
        if (dbUnit is null) return BadRequest();

        dbUnit.Number = unit.Number;
        dbUnit.Price = unit.Price;
        dbUnit.StatusId = unit.StatusId;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Create(UnitPostRequest postData)
    {
        Unit newUnit = new Unit
        {
            Number = postData.Number, Price = postData.Price, StatusId = postData.StatusId
        };
        await _context.Units.AddAsync(newUnit);
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
        var unitToDelete = _context.Units.FirstOrDefault(unit => unit.Id == id);
        if (unitToDelete is null) return BadRequest();

        _context.Units.Remove(unitToDelete);

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, UnitPatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var unitToPatch = _context.Units.FirstOrDefault(unit => unit.Id == id);
        if (unitToPatch is null) return BadRequest();

        unitToPatch.Number = patchData.Number ?? unitToPatch.Number;
        unitToPatch.Price = patchData.Price ?? unitToPatch.Price;
        unitToPatch.StatusId = patchData.StatusId ?? unitToPatch.StatusId;
        await _context.SaveChangesAsync();

        return Ok();
    }
}