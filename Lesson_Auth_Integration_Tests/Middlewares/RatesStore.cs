namespace Lesson_Auth_Integration_Tests.Middlewares;

public class RatesStore
{
    public Dictionary<string, (DateTime LastRequestTime, int RequestCount)> ClientRequests { get; } = new();
}
