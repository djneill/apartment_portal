using apartment_portal_api.Abstractions;
using apartment_portal_api.Models;
using apartment_portal_api.Models.Units;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
    public UnitController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UnitResponse>> GetById(int id)
    {
        var unit = await _unitOfWork.UnitRepository.GetAsync(id);
        if (unit is null) return BadRequest();


        return Ok(unit);
    }

    [HttpGet("/Units")]
    public async Task<ActionResult<ICollection<Unit>>> Get()
    {
        return Ok(await _unitOfWork.UnitRepository.GetAsync());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Unit unit)
    {
        if (id != unit.Id) return BadRequest();

        var dbUnit = await _unitOfWork.UnitRepository.GetAsync(id);
        if (dbUnit is null) return BadRequest();

        dbUnit.Number = unit.Number;
        dbUnit.Price = unit.Price;
        dbUnit.StatusId = unit.StatusId;
        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Create(UnitPostRequest postData)
    {
        Unit newUnit = new Unit
        {
            Number = postData.Number,
            Price = postData.Price,
            StatusId = postData.StatusId
        };
        await _unitOfWork.UnitRepository.AddAsync(newUnit);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = newUnit.Id },
            newUnit
            );
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var unitToDelete = await _unitOfWork.UnitRepository.GetAsync(id);
        if (unitToDelete is null) return BadRequest();

        _unitOfWork.UnitRepository.Delete(unitToDelete);

        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, UnitPatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var unitToPatch = await _unitOfWork.UnitRepository.GetAsync(id);
        if (unitToPatch is null) return BadRequest();

        unitToPatch.Number = patchData.Number ?? unitToPatch.Number;
        unitToPatch.Price = patchData.Price ?? unitToPatch.Price;
        unitToPatch.StatusId = patchData.StatusId ?? unitToPatch.StatusId;
        await _unitOfWork.SaveAsync();

        return Ok();
    }
}