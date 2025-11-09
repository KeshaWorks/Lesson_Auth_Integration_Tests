using Lesson_Auth_Integration_Tests.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Tests;

public class TodoControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private string _adminToken = string.Empty;

    public TodoControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add("X-API-Key", "TEST_API_KEY_123");
        InitializeAdminTokenAsync().Wait();
    }

    private async Task InitializeAdminTokenAsync()
    {
        // Login as admin to get token
        var loginRequest = new LoginRequest(
            Email: "admin@example.com",
            Password: "Admin123!"
        );

        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

        _adminToken = result?["token"] ?? string.Empty;
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken);
    }

    [Fact]
    public async Task AdminOnlyEndpoint_ShouldReturnSuccess_ForAdmin()
    {
        // Act
        var response = await _client.GetAsync("/api/todo/admin");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Admin", content);
    }

    [Fact]
    public async Task CreateTodo_ShouldSucceed_WithValidToken()
    {
        // Act
        var response = await _client.PostAsJsonAsync("/api/todo", "Test Todo");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetAllTodos_ShouldReturnUserTodos()
    {
        // Create a todo first
        await _client.PostAsJsonAsync("/api/todo", "Integration Test Todo");

        // Act
        var response = await _client.GetAsync("/api/todo");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var todos = JsonConvert.DeserializeObject<List<TodoItemDto>>(content);

        Assert.NotNull(todos);
        Assert.True(todos.Count > 0);
        Assert.Contains(todos, t => t.Title == "Integration Test Todo");
    }
}