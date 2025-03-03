using apartment_portal_api.Models.Units;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase
{
    private List<Unit> _units = [
        new Unit(1, 103, 2500, 1, 1), 
        new Unit(2, 592, 3000, 1, 1), 
        new Unit(3, 953, 3250, 2, 2)
    ];


    [HttpGet("{id:int}")]
    public ActionResult<Unit> GetUnitById(int id)
    {
        var unit = _units.FirstOrDefault(x => x.Id == id);

        if (unit is not null) return Ok(unit);

        return NotFound();
    }

    [HttpGet("/Units")]
    public ActionResult<ICollection<Unit>> GetUnits()
    {
        return Ok(_units);
    }

    [HttpPut("{id:int}")]
    public ActionResult EditUnit(int id, Unit unit)
    {
        if (id != unit.Id) return BadRequest();

        var dbUnit = _units.FirstOrDefault(x => x.Id == id);
        if (dbUnit is null) return BadRequest();

        dbUnit.Number = unit.Number;
        dbUnit.Price = unit.Price;
        dbUnit.StatusId = unit.StatusId;
        // Save();
        return Ok();
    }

    [HttpPost]
    public ActionResult PostUnit(UnitPostRequest postData)
    {
        Unit newUnit = new Unit(4, postData.Number, postData.Price, postData.StatusId, 1);
        _units.Add(newUnit);
        // Save();

        return CreatedAtAction(
            nameof(GetUnitById),
            new {id = newUnit.Id},
            newUnit
            );
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteUnit(int id)
    {
        var unitToDelete = _units.FirstOrDefault(unit => unit.Id == id);
        if (unitToDelete is null) return BadRequest();

        _units.Remove(unitToDelete);
        
        // Save();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public ActionResult PatchUnit(int id, UnitPatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var unitToPatch = _units.FirstOrDefault(unit => unit.Id == id);
        if (unitToPatch is null) return BadRequest();

        unitToPatch.Number = patchData.Number ?? unitToPatch.Number;
        unitToPatch.Price = patchData.Price ?? unitToPatch.Price;
        unitToPatch.StatusId = patchData.StatusId ?? unitToPatch.StatusId;
        // Save()

        return Ok();
    }
}