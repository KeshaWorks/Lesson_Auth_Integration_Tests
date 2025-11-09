using Lesson_Auth_Integration_Tests.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests;

public class RateLimitingMiddlewareTests
{
    private readonly Mock<RequestDelegate> _nextMock;
    private readonly Mock<ILogger<RateLimitingMiddleware>> _loggerMock;
    private readonly RatesStore _ratesStore;

    public RateLimitingMiddlewareTests()
    {
        _nextMock = new Mock<RequestDelegate>();
        _loggerMock = new Mock<ILogger<RateLimitingMiddleware>>();
        _ratesStore = new RatesStore();
    }

    [Fact]
    public async Task InvokeAsync_ShouldAllowRequest_WhenUnderLimit()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("192.168.1.1");

        var settings = Options.Create(new RateLimitingSettings { RequestsPerMinute = 5 });
        var middleware = new RateLimitingMiddleware(
            _nextMock.Object,
            _ratesStore,
            settings,
            _loggerMock.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(200, context.Response.StatusCode);
        _nextMock.Verify(x => x(context), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_ShouldReturn429_WhenOverLimit()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("192.168.1.1");

        var settings = Options.Create(new RateLimitingSettings { RequestsPerMinute = 1 });
        var middleware = new RateLimitingMiddleware(
            _nextMock.Object,
            _ratesStore,
            settings,
            _loggerMock.Object);

        // First request
        await middleware.InvokeAsync(context);

        // Second request
        var context2 = new DefaultHttpContext();
        context2.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("192.168.1.1");

        // Act
        await middleware.InvokeAsync(context2);

        // Assert
        Assert.Equal(429, context2.Response.StatusCode);
        _nextMock.Verify(x => x(It.IsAny<HttpContext>()), Times.Once);
    }
}
