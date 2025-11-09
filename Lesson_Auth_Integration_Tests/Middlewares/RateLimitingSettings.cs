namespace Lesson_Auth_Integration_Tests.Middlewares;
public class RateLimitingSettings
{
    public int RequestsPerMinute { get; set; } = 500;
}
