using System.Security.Claims;
using apartment_portal_api.Abstractions;
using apartment_portal_api.DTOs;
using apartment_portal_api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace apartment_portal_api.Controllers;

[Authorize]
public class UnitController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UnitController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UnitDTO>> GetById(int id)
    {
        var unit = await _unitOfWork.UnitRepository.GetAsync(id);
        if (unit is null)
            return NotFound();

        // Check if the current user is associated with this unit (via UnitUser) or is an admin.
        var authResult = await IsUserOrAdmin(unit.Id);
        if (authResult is not null)
            return authResult;

        unit.Status = await _unitOfWork.StatusRepository.GetAsync(unit.StatusId);
        var unitDTO = _mapper.Map<UnitDTO>(unit);
        return Ok(unitDTO);
    }

    // Non-admin users see only units they are associated with; admins see all.
    [HttpGet]
    public async Task<ActionResult<ICollection<UnitDTO>>> Get()
    {
        var units = await _unitOfWork.UnitRepository.GetAsync();

        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim is null)
            return Unauthorized();

        int currentUserId = int.Parse(userClaim.Value);
        bool isAdmin = User.IsInRole("Admin");

        if (!isAdmin)
        {
            var accessibleUnits = new List<Unit>();
            foreach (var unit in units)
            {
                var authResult = await IsUserOrAdmin(unit.Id);
                if (authResult is null)
                    accessibleUnits.Add(unit);
            }
            units = accessibleUnits;
        }

        foreach (var unit in units)
        {
            unit.Status = await _unitOfWork.StatusRepository.GetAsync(unit.StatusId);
        }

        var unitDTOs = _mapper.Map<ICollection<UnitDTO>>(units);
        return Ok(unitDTOs);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UnitPutRequestDTO unitPutDTO)
    {
        if (id != unitPutDTO.Id)
            return BadRequest();

        var dbUnit = await _unitOfWork.UnitRepository.GetAsync(id);
        if (dbUnit is null)
            return NotFound();

        var authResult = await IsUserOrAdmin(dbUnit.Id);
        if (authResult is not null)
            return authResult;

        _mapper.Map(unitPutDTO, dbUnit);
        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<UnitDTO>> Create(UnitPostRequestDTO postData)
    {
        // For creation, we assume that any authenticated user can create a unit.
        var newUnit = _mapper.Map<Unit>(postData);
        await _unitOfWork.UnitRepository.AddAsync(newUnit);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = newUnit.Id },
            _mapper.Map<UnitDTO>(newUnit)
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var unitToDelete = await _unitOfWork.UnitRepository.GetAsync(id);
        if (unitToDelete is null)
            return NotFound();

        var authResult = await IsUserOrAdmin(unitToDelete.Id);
        if (authResult is not null)
            return authResult;

        _unitOfWork.UnitRepository.Delete(unitToDelete);
        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, UnitPatchRequestDTO patchData)
    {
        if (id != patchData.Id)
            return BadRequest();

        var unitToPatch = await _unitOfWork.UnitRepository.GetAsync(id);
        if (unitToPatch is null)
            return NotFound();

        var authResult = await IsUserOrAdmin(unitToPatch.Id);
        if (authResult is not null)
            return authResult;

        _mapper.Map(patchData, unitToPatch);
        await _unitOfWork.SaveAsync();
        return Ok();
    }
    private async Task<ActionResult?> IsUserOrAdmin(int unitId)
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim is null)
            return Unauthorized();

        int currentUserId = int.Parse(userClaim.Value);
        if (User.IsInRole("Admin"))
            return null;

        var unitUsers = await _unitOfWork.UnitUserRepository.GetAsync(u => u.UnitId == unitId);
        if (unitUsers.Any(uu => uu.UserId == currentUserId))
            return null;

        return Forbid();
    }
}