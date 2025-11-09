<<<<<<< HEAD
﻿namespace Lesson_Auth_Integration_Tests.Middlewares;
=======
﻿// ====================================================================================================
// Middleware/ApiKeyMiddleware.cs
// ====================================================================================================
namespace Lesson_Auth_Integration_Tests.Middleware;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(
            "Request: {Method} {Path} from {IP}",
            context.Request.Method,
            context.Request.Path,
            context.Connection.RemoteIpAddress);

        var originalBodyStream = context.Response.Body;
        await using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        _logger.LogInformation(
            "Response: {StatusCode} {Content}",
            context.Response.StatusCode,
            responseText);

        await responseBody.CopyToAsync(originalBodyStream);
    }
}
