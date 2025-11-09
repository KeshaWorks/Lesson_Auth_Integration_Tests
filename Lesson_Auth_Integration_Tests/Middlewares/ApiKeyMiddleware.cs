<<<<<<< HEAD
﻿namespace Lesson_Auth_Integration_Tests.Middlewares;

=======
﻿// ====================================================================================================
// Middleware/ApiKeyMiddleware.cs
// ====================================================================================================
namespace Lesson_Auth_Integration_Tests.Middleware;

// SOLID: Single Responsibility - API key validation only
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyMiddleware(
        RequestDelegate next,
        IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-API-Key", out var apiKey) ||
            apiKey != _configuration["ApiKey"])
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "Invalid or missing API key" });
            return;
        }

        await _next(context);
    }
}
