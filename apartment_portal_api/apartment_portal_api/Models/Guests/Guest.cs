using apartment_portal_api.Abstractions;

namespace apartment_portal_api.Models.Guests;

public sealed class Guest: Entity
{
    public Guest(
        int id,
        int userId,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string accessCode,
        DateTime expiration,
        DateTime createdOn) : base(id)

    {
        Id = id;
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        AccessCode = accessCode;
        Expiration = expiration;
        CreatedOn = createdOn
    }
    
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AccessCode { get; set; }
    public DateTime Expiration { get; set; }
    public DateTime CreatedOn { get; set; }
    
    

}