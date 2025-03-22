using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Packages;
using apartment_portal_api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PackageController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PackageController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PackageGetByIdResponse>> GetById(int id)
    {
        var packageList = await _unitOfWork.PackageRepository.GetAsync(
            p => p.Id == id,
            $"{nameof(Package.Unit)},{nameof(Package.Status)}"
        );
        var package = packageList.FirstOrDefault();
        if (package is null)
            return NotFound(package);

        // Retrieve current user's ID and check if the user is authorized
        var currentUserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(currentUserIdStr))
            return Unauthorized();
        int currentUserId = int.Parse(currentUserIdStr);
        bool isAdmin = User.IsInRole("Admin");

        // If not an admin, ensure that the package's unit is associated with the current user.
        if (!isAdmin)
        {
            var unitUsers = await _unitOfWork.UnitUserRepository.GetAsync(u => u.UnitId == package.UnitId);
            if (!unitUsers.Any(uu => uu.UserId == currentUserId))
                return Forbid();
        }

        var packageDto = _mapper.Map<PackageGetByIdResponse>(package);
        return Ok(packageDto);
    }

    [HttpGet("/Packages")]
    public async Task<ActionResult<ICollection<PackageGetResponse>>> Get(int userId = 0, int statusId = 0)
    {
        var currentUserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(currentUserIdStr))
            return Unauthorized();
        int currentUserId = int.Parse(currentUserIdStr);
        bool isAdmin = User.IsInRole("Admin");

        // For non-admins, if a userId filter is provided it must match the logged-in user.
        if (!isAdmin)
        {
            if (userId != 0 && userId != currentUserId)
                return Forbid();
            userId = currentUserId;
        }

        var packages = await _unitOfWork.PackageRepository.GetByUserId(userId, statusId);
        var packageDtos = _mapper.Map<ICollection<PackageGetResponse>>(packages);
        return Ok(packageDtos);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Update(int id, PackagePutRequest package)
    {
        if (id != package.Id)
            return BadRequest();

        var dbPackage = await _unitOfWork.PackageRepository.GetAsync(id);
        if (dbPackage is null)
            return BadRequest();

        dbPackage.LockerNumber = package.LockerNumber;
        dbPackage.StatusId = package.StatusId;
        dbPackage.UnitId = package.UnitId;
        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Create(PackagePostRequest postData)
    {
        var newPackage = _mapper.Map<Package>(postData);
        newPackage.Code = AccessCodeGenerator.GenerateAccessCode();

        await _unitOfWork.PackageRepository.AddAsync(newPackage);
        await _unitOfWork.SaveAsync();

        return Created("", null);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        var packageToDelete = await _unitOfWork.PackageRepository.GetAsync(id);
        if (packageToDelete is null)
            return NotFound();

        _unitOfWork.PackageRepository.Delete(packageToDelete);
        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Patch(int id, PackagePatchRequest patchData)
    {
        if (id != patchData.Id)
            return BadRequest();

        var packageToPatch = await _unitOfWork.PackageRepository.GetAsync(id);
        if (packageToPatch is null)
            return BadRequest();

        packageToPatch.LockerNumber = patchData.LockerNumber ?? packageToPatch.LockerNumber;
        packageToPatch.StatusId = patchData.StatusId ?? packageToPatch.StatusId;
        packageToPatch.UnitId = patchData.UnitId ?? packageToPatch.UnitId;

        await _unitOfWork.SaveAsync();
        return Ok();
    }
}