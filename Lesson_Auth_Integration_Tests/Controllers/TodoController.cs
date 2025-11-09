using Lesson_Auth_Integration_Tests.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lesson_Auth_Integration_Tests.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TodoController(
        ITodoService todoService,
        IHttpContextAccessor httpContextAccessor)
    {
        _todoService = todoService;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Admin-only endpoint (Role-based authorization)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminOnly()
    {
        return Ok("Hello, Admin!");
    }

    /// <summary>
    /// Create todo (Policy-based authorization)
    /// </summary>
    [Authorize(Policy = "CanCreateTodo")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string title)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var todo = await _todoService.CreateTodoAsync(userId, title);
        return CreatedAtAction(nameof(GetAll), new { id = todo.Id }, todo);
    }

    /// <summary>
    /// Get all todos for current user
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var todos = await _todoService.GetTodosForUserAsync(userId);
        return Ok(todos);
    }

    /// <summary>
    /// Delete todo
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var success = await _todoService.DeleteTodoAsync(id, userId);
        return success ? NoContent() : NotFound();
    }
}