using Lesson_Auth_Integration_Tests.DTOs;
using Lesson_Auth_Integration_Tests.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_Auth_Integration_Tests.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        return result.Succeeded
            ? Ok(new { message = "User registered successfully" })
            : BadRequest(result.Errors);
    }

    /// <summary>
    /// Login and get JWT token
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);
        return token == null
            ? Unauthorized()
            : Ok(new { token });
    }
}
