<<<<<<< HEAD
﻿namespace Lesson_Auth_Integration_Tests.Middlewares;
=======
﻿// ====================================================================================================
// Middleware/ApiKeyMiddleware.cs
// ====================================================================================================
namespace Lesson_Auth_Integration_Tests.Middleware;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e

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