using apartment_portal_api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace apartment_portal_api.Controllers;
[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    [HttpGet("{id:int}")]
    public ActionResult<Package> GetPackageById(int id)
    {
        Random random = new();

        if (id == 1)
        {
            return Ok(new Package
            {
                Id = 1,
                UnitUsersId = random.Next(),
                LockerNumber = random.Next(1,101),
                Code = random.Next(100000,1000000)
            });
        }

        return NotFound();
    }

    [HttpGet("/Packages")]
    public ActionResult<ICollection<Package>> GetPackages()
    {
        Random random = new();

        Package package1 = new()
        {
            Id = 1,
            UnitUsersId = random.Next(),
            LockerNumber = random.Next(1,101),
            Code = random.Next(100000,1000000)
        };

        Package package2 = new()
        {
            Id = 2,
            UnitUsersId = random.Next(),
            LockerNumber = random.Next(1,101),
            Code = random.Next(100000,1000000)
        };

        Package package3 = new()
        {
            Id = 3,
            UnitUsersId = random.Next(),
            LockerNumber = random.Next(1,101),
            Code = random.Next(100000,1000000)
        };

        Package package4 = new()
        {
            Id = 4,
            UnitUsersId = random.Next(),
            LockerNumber = random.Next(1,101),
            Code = random.Next(100000,1000000)
        };

        Package package5 = new()
        {
            Id = 5,
            UnitUsersId = random.Next(),
            LockerNumber = random.Next(1,101),
            Code = random.Next(100000,1000000)
        };

        Package package6 = new()
        {
            Id = 6,
            UnitUsersId = random.Next(),
            LockerNumber = random.Next(1,101),
            Code = random.Next(100000,1000000)
        };

        Package package7 = new()
        {
            Id = 7,
            UnitUsersId = random.Next(),
            LockerNumber = random.Next(1,101),
            Code = random.Next(100000,1000000)
        };

        Package package8 = new()
        {
            Id = 8,
            UnitUsersId = random.Next(),
            LockerNumber = random.Next(1,101),
            Code = random.Next(100000,1000000)
        };

        Package[] res = [package1, package2, package3, package4, package5, package6, package7, package8];

        return Ok(res);
    }
}
