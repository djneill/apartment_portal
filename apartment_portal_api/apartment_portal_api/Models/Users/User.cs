using apartment_portal_api.Abstractions;

namespace apartment_portal_api.Models.Users;

public sealed class User : Entity
{
    public User(
        int id,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        int statusId,
        DateTime createdOn,
        int createdBy,
        DateTime modifiedOn,
        int modifiedBy)
        : base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        StatusId = statusId;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        ModifiedOn = modifiedOn;
        ModifiedBy = modifiedBy;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }






    public static List<User> Users =
    [
        new User(1, "John", "Doe", DateTime.Now, 1, DateTime.Now, 2, DateTime.Now, 2),
        new User(2, "Luffy", "Monkey", DateTime.Now, 1, DateTime.Now, 1, DateTime.Now, 1),
        new User(3, "Portgas", "Ace", DateTime.Now, 2, DateTime.Now, 2, DateTime.Now, 2)
    ];
}