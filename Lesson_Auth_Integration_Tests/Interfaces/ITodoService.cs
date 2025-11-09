<<<<<<< HEAD
﻿using Lesson_Auth_Integration_Tests.DTOs;
=======
﻿// ====================================================================================================
// Interfaces/IAuthService.cs
// ====================================================================================================
using Lesson_Auth_Integration_Tests.DTOs;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e

namespace Lesson_Auth_Integration_Tests.Interfaces;

public interface ITodoService
{
    Task<TodoItemDto> CreateTodoAsync(string userId, string title);
    Task<List<TodoItemDto>> GetTodosForUserAsync(string userId);
    Task<bool> DeleteTodoAsync(int id, string userId);
}
