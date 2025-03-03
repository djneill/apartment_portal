using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.Units;
using apartment_portal_api.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase
{
    [HttpGet("{id:int}")]
    public ActionResult<UnitResponse> GetById(int id)
    {
        var unit = Unit.Units.FirstOrDefault(x => x.Id == id);
        if (unit is null) return BadRequest();

        var status = Status.Statuses.FirstOrDefault(s => s.Id == unit.StatusId);
        if (status is null) return BadRequest();

        List<UserResponse> users = [];

        foreach (User u in Models.Users.User.Users)
        {
            var stat = Status.Statuses.FirstOrDefault(s => s.Id == u.StatusId);
            if (stat is null || stat.Id == 2) continue;

            users.Add(new UserResponse(u.Id, u.FirstName, u.LastName, u.DateOfBirth, stat));
        }

        UnitResponse res = new(unit.Id, unit.Number, unit.Price, users, status);


        return Ok(res);
    }

    [HttpGet("/Units")]
    public ActionResult<ICollection<Unit>> Get()
    {
        return Ok(Unit.Units);
    }

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, Unit unit)
    {
        if (id != unit.Id) return BadRequest();

        var dbUnit = Unit.Units.FirstOrDefault(x => x.Id == id);
        if (dbUnit is null) return BadRequest();

        dbUnit.Number = unit.Number;
        dbUnit.Price = unit.Price;
        dbUnit.StatusId = unit.StatusId;
        // Save();
        return Ok();
    }

    [HttpPost]
    public ActionResult Create(UnitPostRequest postData)
    {
        Unit newUnit = new Unit(4, postData.Number, postData.Price, postData.StatusId, 1);
        Unit.Units.Add(newUnit);
        // Save();

        return CreatedAtAction(
            nameof(GetById),
            new { id = newUnit.Id },
            newUnit
            );
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var unitToDelete = Unit.Units.FirstOrDefault(unit => unit.Id == id);
        if (unitToDelete is null) return BadRequest();

        Unit.Units.Remove(unitToDelete);

        // Save();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public ActionResult Patch(int id, UnitPatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var unitToPatch = Unit.Units.FirstOrDefault(unit => unit.Id == id);
        if (unitToPatch is null) return BadRequest();

        unitToPatch.Number = patchData.Number ?? unitToPatch.Number;
        unitToPatch.Price = patchData.Price ?? unitToPatch.Price;
        unitToPatch.StatusId = patchData.StatusId ?? unitToPatch.StatusId;
        // Save()

        return Ok();
    }
}