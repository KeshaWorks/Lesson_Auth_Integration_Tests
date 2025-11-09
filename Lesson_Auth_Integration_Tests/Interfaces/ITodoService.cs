using Lesson_Auth_Integration_Tests.DTOs;

namespace Lesson_Auth_Integration_Tests.Interfaces;

public interface ITodoService
{
    Task<TodoItemDto> CreateTodoAsync(string userId, string title);
    Task<List<TodoItemDto>> GetTodosForUserAsync(string userId);
    Task<bool> DeleteTodoAsync(int id, string userId);
}
