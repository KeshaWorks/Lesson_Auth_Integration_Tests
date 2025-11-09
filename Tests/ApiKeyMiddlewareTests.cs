using Lesson_Auth_Integration_Tests.Middleware;
<<<<<<< HEAD
using Lesson_Auth_Integration_Tests.Middlewares;
=======
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests;

public class ApiKeyMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_ShouldReturn401_WhenApiKeyMissing()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var next = new Mock<RequestDelegate>();
        var configMock = new Mock<IConfiguration>();
        configMock.SetupGet(x => x["ApiKey"]).Returns("VALID_KEY");

        var middleware = new ApiKeyMiddleware(next.Object, configMock.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(401, context.Response.StatusCode);
        next.Verify(x => x(It.IsAny<HttpContext>()), Times.Never);
    }

    [Fact]
    public async Task InvokeAsync_ShouldContinue_WhenValidApiKey()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers["X-API-Key"] = "VALID_KEY";

        var next = new Mock<RequestDelegate>();
        var configMock = new Mock<IConfiguration>();
        configMock.SetupGet(x => x["ApiKey"]).Returns("VALID_KEY");

        var middleware = new ApiKeyMiddleware(next.Object, configMock.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        next.Verify(x => x(context), Times.Once);
    }
}
