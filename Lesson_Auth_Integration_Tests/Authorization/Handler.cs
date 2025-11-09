using Microsoft.AspNetCore.Authorization;

namespace Lesson_Auth_Integration_Tests.Authorization;

public class ApiKeyAuthorizationHandler : AuthorizationHandler<ApiKeyRequirement>
{
    private readonly IConfiguration _configuration;

    public ApiKeyAuthorizationHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ApiKeyRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
        {
            return Task.CompletedTask;
        }

        if (httpContext.Request.Headers.TryGetValue("X-API-Key", out var apiKey))
        {
            var validKey = _configuration["ApiKey"];
            if (!string.IsNullOrEmpty(validKey) && apiKey == validKey)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}