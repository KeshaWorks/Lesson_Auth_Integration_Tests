<<<<<<< HEAD
﻿namespace Lesson_Auth_Integration_Tests.Middlewares;
=======
﻿// ====================================================================================================
// Middleware/ApiKeyMiddleware.cs
// ====================================================================================================
namespace Lesson_Auth_Integration_Tests.Middleware;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e

public static class ApiKeyMiddlewareExtensions
{
    public static IApplicationBuilder UseApiKey(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyMiddleware>();
    }
}
