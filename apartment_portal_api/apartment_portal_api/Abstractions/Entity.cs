namespace apartment_portal_api.Abstractions;

public class Entity
{
    protected Entity(int id)
    {
        Id = id;
    }

    protected Entity()
    {

    }

    public int Id { get; init; }
}