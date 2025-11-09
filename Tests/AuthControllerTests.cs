using Lesson_Auth_Integration_Tests.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Tests;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add("X-API-Key", "TEST_API_KEY_123");

    }

    [Fact]
    public async Task Register_ShouldReturnSuccess_WhenValidCredentials()
    {
        // Arrange
        var request = new RegisterRequest(
            Email: "newuser@example.com",
            Password: "newpass123",
            FullName: "New User"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("successfully", responseContent);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenInvalidEmail()
    {
        // Arrange
        var request = new RegisterRequest(
            Email: "invalid-email",
            Password: "pass123",
            FullName: "Invalid User"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenValidCredentials()
    {
        // First register a user
        var registerRequest = new RegisterRequest(
            Email: "testlogin@example.com",
            Password: "loginpass123",
            FullName: "Login Test"
        );
        await _client.PostAsJsonAsync("/api/auth/register", registerRequest);

        // Then login
        var loginRequest = new LoginRequest(
            Email: "testlogin@example.com",
            Password: "loginpass123"
        );

        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

        Assert.NotNull(result);
        Assert.True(result.ContainsKey("token"));
        Assert.False(string.IsNullOrEmpty(result["token"]));
    }
}
