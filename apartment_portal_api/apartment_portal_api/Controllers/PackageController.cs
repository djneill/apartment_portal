using apartment_portal_api.Models.Packages;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;
[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private List<Package> _packages = [new Package(1, 1, 12, 827294, 1), new Package(2, 1, 12, 827294, 1), new Package(2, 1, 12, 827294, 2)];


    [HttpGet("{id:int}")]
    public ActionResult<Package> GetPackageById(int id)
    {
        var package = _packages.FirstOrDefault(x => x.Id == id);

        if (package is not null) return Ok(package);

        return NotFound();
    }

    [HttpGet("/Packages")]
    public ActionResult<ICollection<Package>> GetPackages()
    {
        return Ok(_packages);
    }
}
