using apartment_portal_api.Models.Statuses;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    [HttpGet("{id:int}")]
    public ActionResult<Status> GetById(int id)
    {
        var status = Status.Statuses.FirstOrDefault(x => x.Id == id);
        if (status is null) return NotFound(status);

        return Ok(status);
    }

    [HttpGet("/Statuses")]
    public ActionResult<ICollection<Status>> Get()
    {
        return Ok(Status.Statuses);
    }

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, Status status)
    {
        if (id != status.Id) return BadRequest();

        var dbStatus = Status.Statuses.FirstOrDefault(x => x.Id == id);
        if (dbStatus is null) return BadRequest();

        dbStatus.Name = status.Name;
        // Save();
        return Ok();
    }

    [HttpPost]
    public ActionResult Create(StatusPostRequest postData)
    {
        Status newUnit = new Status(4, postData.Name);
        Status.Statuses.Add(newUnit);
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
        var statusToDelete = Status.Statuses.FirstOrDefault(unit => unit.Id == id);
        if (statusToDelete is null) return BadRequest();

        Status.Statuses.Remove(statusToDelete);

        // Save();
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public ActionResult Patch(int id, StatusPatchRequest patchData)
    {
        if (id != patchData.Id) return BadRequest();

        var statusToPatch = Status.Statuses.FirstOrDefault(status => status.Id == id);
        if (statusToPatch is null) return BadRequest();

        statusToPatch.Name = patchData.Name ?? statusToPatch.Name;
        // Save()

        return Ok();
    }
}
