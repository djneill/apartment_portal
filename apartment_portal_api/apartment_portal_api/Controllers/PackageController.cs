using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Packages;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;
[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
    public PackageController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<PackageGetByIdResponse>> GetById(int id)
    {
        var package = await _unitOfWork.PackageRepository.GetAsync(id);
        if (package is null) return NotFound(package);

        return Ok(package);
    }

    [HttpGet("/Packages")]
    public async Task<ActionResult<ICollection<PackageGetResponse>>> Get()
    {

        return Ok(await _unitOfWork.PackageRepository.GetAsync());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Package package)
    {
        if (id != package.Id) return BadRequest();

        var dbPackage = await _unitOfWork.PackageRepository.GetAsync(id);
        if (dbPackage is null) return BadRequest();

        dbPackage.LockerNumber = package.LockerNumber;
        dbPackage.Code = package.Code;
        dbPackage.StatusId = package.StatusId;
        dbPackage.UnitId = package.UnitId;
        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Create(PackagePostRequest postData)
    {
        Package newPackage = new Package
        { UnitId = postData.UnitUsersId, LockerNumber = postData.LockerNumber, Code = postData.Code, StatusId = postData.StatusId };

        await _unitOfWork.PackageRepository.AddAsync(newPackage);

        await _unitOfWork.SaveAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = newPackage.Id },
            newPackage
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var packageToDelete = await _unitOfWork.PackageRepository.GetAsync(id);
        if (packageToDelete is null) return BadRequest();

        _unitOfWork.PackageRepository.Delete(packageToDelete);

        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, PackagePatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var packageToPatch = await _unitOfWork.PackageRepository.GetAsync(id);
        if (packageToPatch is null) return BadRequest();

        packageToPatch.LockerNumber = patchData.LockerNumber ?? packageToPatch.LockerNumber;
        packageToPatch.Code = patchData.Code ?? packageToPatch.Code;
        packageToPatch.StatusId = patchData.StatusId ?? packageToPatch.StatusId;
        packageToPatch.UnitId = patchData.UnitUsersId ?? packageToPatch.UnitId;

        await _unitOfWork.SaveAsync();

        return Ok();
    }
}
