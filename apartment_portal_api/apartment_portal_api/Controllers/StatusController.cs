﻿using apartment_portal_api.Abstractions;
using apartment_portal_api.DTOs;
using apartment_portal_api.Models.Statuses;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace apartment_portal_api.Controllers;

[Route("[controller]"), ApiController]
public class StatusController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StatusController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StatusDTO>> GetById(int id)
    {
        var status = await _unitOfWork.StatusRepository.GetAsync(id);
        if (status is null) return NotFound(status);

        var statusDTO = _mapper.Map<StatusDTO>(status);
        return Ok(statusDTO);
    }

    [HttpGet("/Statuses")]
    public async Task<ActionResult<ICollection<StatusDTO>>> Get()
    {
        var statuses = await _unitOfWork.StatusRepository.GetAsync();
        var statusDTOs = _mapper.Map<ICollection<StatusDTO>>(statuses);
        return Ok(statusDTOs);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Status status)
    {
        if (id != status.Id) return BadRequest();

        var dbStatus = await _unitOfWork.StatusRepository.GetAsync(id);
        if (dbStatus is null) return BadRequest();

        dbStatus.Name = status.Name;

        await _unitOfWork.SaveAsync();

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Create(StatusPostRequest postData)
    {
        Status newStatus = _mapper.Map<Status>(postData);
        await _unitOfWork.StatusRepository.AddAsync(newStatus);

        await _unitOfWork.SaveAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = newStatus.Id },
            newStatus
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var statusToDelete = await _unitOfWork.StatusRepository.GetAsync(id);
        if (statusToDelete is null) return BadRequest();

        _unitOfWork.StatusRepository.Delete(statusToDelete);

        await _unitOfWork.SaveAsync();

        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, StatusPatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var statusToPatch = await _unitOfWork.StatusRepository.GetAsync(id);
        if (statusToPatch is null) return BadRequest();

        statusToPatch.Name = patchData.Name ?? statusToPatch.Name;

        await _unitOfWork.SaveAsync();

        return Ok();
    }
}