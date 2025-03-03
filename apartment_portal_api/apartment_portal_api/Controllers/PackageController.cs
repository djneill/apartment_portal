using apartment_portal_api.Data;
using apartment_portal_api.Models.Packages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Controllers;
[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private PostgresContext _context;
    public PackageController(PostgresContext context)
    {
        _context = context;
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<PackageGetByIdResponse>> GetById(int id)
    {
        var package = await _context.Packages.FirstOrDefaultAsync(x => x.Id == id);
        if (package is null) return NotFound(package);

        

        return Ok(package);
    }

    [HttpGet("/Packages")]
    public async Task<ActionResult<ICollection<PackageGetResponse>>> Get()
    {

        return Ok(await _context.Packages.ToListAsync());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Package package)
    {
        if (id != package.Id) return BadRequest();

        var dbPackage = await _context.Packages.FirstOrDefaultAsync(x => x.Id == id);
        if (dbPackage is null) return BadRequest();

        dbPackage.LockerNumber = package.LockerNumber;
        dbPackage.Code = package.Code;
        dbPackage.StatusId = package.StatusId;
        dbPackage.UnitId = package.UnitId;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Create(PackagePostRequest postData)
    {
        Package newUnit = new Package
            { UnitId = postData.UnitUsersId, LockerNumber = postData.LockerNumber, Code = postData.Code, StatusId = postData.StatusId };
        await _context.Packages.AddAsync(newUnit);

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
        var packageToDelete = await _context.Packages.FirstOrDefaultAsync(unit => unit.Id == id);
        if (packageToDelete is null) return BadRequest();

        _context.Packages.Remove(packageToDelete);

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, PackagePatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var packageToPatch = await _context.Packages.FirstOrDefaultAsync(package => package.Id == id);
        if (packageToPatch is null) return BadRequest();

        packageToPatch.LockerNumber = patchData.LockerNumber ?? packageToPatch.LockerNumber;
        packageToPatch.Code = patchData.Code ?? packageToPatch.Code;
        packageToPatch.StatusId = patchData.StatusId ?? packageToPatch.StatusId;
        packageToPatch.UnitId = patchData.UnitUsersId ?? packageToPatch.UnitId;

        await _context.SaveChangesAsync();

        return Ok();
    }
}
