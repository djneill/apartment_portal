using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Statuses;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;


[Route("[controller]"), ApiController]
public class StatusController : ControllerBase
{
    private IUnitOfWork _unitOfWork;

    public StatusController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Status>> GetById(int id)
    {
        var status = await _unitOfWork.StatusRepository.GetAsync(id);
        if (status is null) return NotFound(status);

        return Ok(status);
    }

    [HttpGet("/Statuses")]
    public async Task<ActionResult<ICollection<Status>>> Get()
    {
        return Ok(await _unitOfWork.StatusRepository.GetAsync());
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
        Status newStatus = new Status { Name = postData.Name };
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
