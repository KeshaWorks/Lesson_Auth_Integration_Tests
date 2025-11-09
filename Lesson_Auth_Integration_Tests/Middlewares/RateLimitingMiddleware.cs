using Microsoft.Extensions.Options;

namespace Lesson_Auth_Integration_Tests.Middlewares;
public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RatesStore _ratesStore;
    private readonly RateLimitingSettings _settings;
    private readonly ILogger<RateLimitingMiddleware> _logger;

    public RateLimitingMiddleware(
        RequestDelegate next,
        RatesStore ratesStore,
        IOptions<RateLimitingSettings> settings,
        ILogger<RateLimitingMiddleware> logger)
    {
        _next = next;
        _ratesStore = ratesStore;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        lock (_ratesStore.ClientRequests)
        {
            if (!_ratesStore.ClientRequests.TryGetValue(ipAddress, out var clientData))
            {
                clientData = (DateTime.UtcNow, 0);
            }

            var (lastRequestTime, requestCount) = clientData;
            var now = DateTime.UtcNow;
            if ((now - lastRequestTime).TotalMinutes >= 1)
            {
                requestCount = 0;
            }

            if (requestCount >= _settings.RequestsPerMinute)
            {
                context.Response.StatusCode = 429;
                _logger.LogWarning("Rate limit exceeded for IP: {IP}", ipAddress);
                context.Response.WriteAsJsonAsync(new { error = "Too many requests" });
                return;
            }

            requestCount++;
            _ratesStore.ClientRequests[ipAddress] = (now, requestCount);
        }

        await _next(context);
    }
}
