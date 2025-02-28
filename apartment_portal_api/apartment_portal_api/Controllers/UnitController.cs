using apartment_portal_api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase
{
    [HttpGet("{id:int}")]
    public ActionResult<Unit> GetUnitById(int id)
    {
        if (id == 1)
        {
            return Ok(new Unit()
            {
                Id = 1,
                Number = 2,
                Price = 2500
            });
        }

        return NotFound();
    }

    [HttpGet("/Units")]
    public ActionResult<ICollection<Unit>> GetUnits()
    {
        Unit unit1 = new()
        {
            Id = 1,
            Number = 359,
            Price = 2500
        };

        Unit unit2 = new()
        {
            Id = 2,
            Number = 204,
            Price = 3200
        };

        Unit unit3 = new()
        {
            Id = 3,
            Number = 352,
            Price = 2500
        };

        Unit unit4 = new()
        {
            Id = 4,
            Number = 837,
            Price = 3200
        };

        Unit unit5 = new()
        {
            Id = 5,
            Number = 725,
            Price = 2500
        };

        Unit unit6 = new()
        {
            Id = 6,
            Number = 194,
            Price = 3200
        };

        Unit unit7 = new()
        {
            Id = 7,
            Number = 062,
            Price = 2500
        };

        Unit unit8 = new()
        {
            Id = 8,
            Number = 103,
            Price = 3200
        };

        Unit[] res = [unit1, unit2, unit3, unit4, unit5, unit6, unit7, unit8];

        return Ok(res);
    }
}