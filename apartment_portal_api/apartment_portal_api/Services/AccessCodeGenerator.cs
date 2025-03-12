namespace apartment_portal_api.Services;

public class AccessCodeGenerator
{
    private static Random _random = new();
    private static int _upperBound = 1000000;
    private static int _lowerBound = 100000;

    public static int GenerateAccessCode()
    {
        return _random.Next(_lowerBound, _upperBound);
    }
}