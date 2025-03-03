using apartment_portal_api.Abstractions;

namespace apartment_portal_api.Models.User;

public sealed class User : Entity
{
    public User(
        int id,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        int statusId)
        : base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        StatusId = statusId;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int StatusId { get; set; }








    public static List<User> Users =
    [
        new User(1, "John", "Doe", DateTime.Now, 1),
        new User(2, "Luffy", "Monkey", DateTime.Now, 1),
        new User(3, "Portgas", "Ace", DateTime.Now, 2)
    ];
}