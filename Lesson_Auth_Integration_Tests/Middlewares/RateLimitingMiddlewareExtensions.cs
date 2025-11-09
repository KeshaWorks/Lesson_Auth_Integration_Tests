using Lesson_Auth_Integration_Tests.Middlewares.Lesson_Auth_Integration_Tests.Middleware;

namespace Lesson_Auth_Integration_Tests.Middlewares;

public static class RateLimitingMiddlewareExtensions
{
    public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RateLimitingMiddleware>();
    }

    public static IServiceCollection ConfigureRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<RateLimitingSettings>(
            configuration.GetSection("RateLimiting"));
    }
}