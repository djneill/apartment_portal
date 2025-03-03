using apartment_portal_api.Models.Packages;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;
[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    [HttpGet("{id:int}")]
    public ActionResult<PackageGetByIdResponse> GetById(int id)
    {
        var package = Package.Packages.FirstOrDefault(x => x.Id == id);
        if (package is null) return NotFound(package);

        var status = Status.Statuses.FirstOrDefault(status => status.Id == package.StatusId);
        if (status is null) return BadRequest();

        List<UserResponse> tenants = [];

        foreach (User u in Models.User.User.Users)
        {
            var tenantStatus = Status.Statuses.FirstOrDefault(s => s.Id == u.StatusId);
            if (tenantStatus is null || tenantStatus.Id == 2) continue;

            tenants.Add(new UserResponse(u.Id, u.FirstName, u.LastName, u.DateOfBirth,
                tenantStatus));
        }

        PackageGetByIdResponse res = new(package.Id, package.LockerNumber, package.Code, status, tenants);

        return Ok(res);
    }

    [HttpGet("/Packages")]
    public ActionResult<ICollection<PackageGetResponse>> Get()
    {
        List<PackageGetResponse> res = [];

        foreach (Package package in Package.Packages)
        {
            var status = Status.Statuses.FirstOrDefault(stat => stat.Id == package.StatusId);
            if (status is null || status.Id == 2) continue;

            res.Add(new PackageGetResponse(package.Id, package.LockerNumber, package.Code, status));
        }

        return Ok(res);
    }

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, Package package)
    {
        if (id != package.Id) return BadRequest();

        var dbPackage = Package.Packages.FirstOrDefault(x => x.Id == id);
        if (dbPackage is null) return BadRequest();

        dbPackage.LockerNumber = package.LockerNumber;
        dbPackage.Code = package.Code;
        dbPackage.StatusId = package.StatusId;
        dbPackage.UnitUsersId = package.UnitUsersId;
        // Save();
        return Ok();
    }

    [HttpPost]
    public ActionResult Create(PackagePostRequest postData)
    {
        Package newUnit = new Package(4, postData.UnitUsersId, postData.LockerNumber, postData.Code, postData.StatusId);
        Package.Packages.Add(newUnit);
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
        var packageToDelete = Package.Packages.FirstOrDefault(unit => unit.Id == id);
        if (packageToDelete is null) return BadRequest();

        Package.Packages.Remove(packageToDelete);

        // Save();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public ActionResult Patch(int id, PackagePatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var packageToPatch = Package.Packages.FirstOrDefault(package => package.Id == id);
        if (packageToPatch is null) return BadRequest();

        packageToPatch.LockerNumber = patchData.LockerNumber ?? packageToPatch.LockerNumber;
        packageToPatch.Code = patchData.Code ?? packageToPatch.Code;
        packageToPatch.StatusId = patchData.StatusId ?? packageToPatch.StatusId;
        packageToPatch.UnitUsersId = patchData.UnitUsersId ?? packageToPatch.UnitUsersId;
        // Save()

        return Ok();
    }
}
