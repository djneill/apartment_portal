using apartment_portal_api.Abstractions;
using apartment_portal_api.DTOs;
using apartment_portal_api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase
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
        if (unit is null) return NotFound();

        var unitDTO = _mapper.Map<UnitDTO>(unit);
        return Ok(unitDTO);
    }

    [HttpGet("/Units")]
    public async Task<ActionResult<ICollection<UnitDTO>>> Get()
    {
        var units = await _unitOfWork.UnitRepository.GetAsync();
        var unitDTOs = _mapper.Map<ICollection<UnitDTO>>(units);
        return Ok(unitDTOs);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UnitDTO unitDTO)
    {
        if (id != unitDTO.Id) return BadRequest();

        var dbUnit = await _unitOfWork.UnitRepository.GetAsync(id);
        if (dbUnit is null) return NotFound();

        _mapper.Map(unitDTO, dbUnit);
        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Create(UnitPostRequestDTO postData)
    {
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
        if (unitToDelete is null) return NotFound();

        _unitOfWork.UnitRepository.Delete(unitToDelete);
        await _unitOfWork.SaveAsync();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, UnitPatchRequestDTO patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var unitToPatch = await _unitOfWork.UnitRepository.GetAsync(id);
        if (unitToPatch is null) return NotFound();

        _mapper.Map(patchData, unitToPatch);
        await _unitOfWork.SaveAsync();

        return Ok();
    }
}