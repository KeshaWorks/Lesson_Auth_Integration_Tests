<<<<<<< HEAD
﻿using AutoMapper;
=======
﻿// ====================================================================================================
// Services/AuthService.cs
// ====================================================================================================
using AutoMapper;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
using Lesson_Auth_Integration_Tests.DTOs;
using Lesson_Auth_Integration_Tests.Interfaces;
using Lesson_Auth_Integration_Tests.Models;
using Microsoft.AspNetCore.Identity;

namespace Lesson_Auth_Integration_Tests.Services;
<<<<<<< HEAD
=======

// SOLID: Single Responsibility - only todo-related logic
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
public class TodoService : ITodoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
<<<<<<< HEAD
=======

    // SOLID: Dependency Inversion
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
    public TodoService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<IdentityUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<TodoItemDto> CreateTodoAsync(string userId, string title)
    {
        var todo = new TodoItem
        {
            Title = title,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.TodoItems.AddAsync(todo);
        await _unitOfWork.CompleteAsync();

        var user = await _userManager.FindByIdAsync(userId);
        return _mapper.Map<TodoItemDto>(todo, opt =>
            opt.Items["OwnerEmail"] = user?.Email ?? "unknown");
    }

    public async Task<List<TodoItemDto>> GetTodosForUserAsync(string userId)
    {
        var todos = await _unitOfWork.TodoItems
            .FindByAsync(t => t.UserId == userId);

        return todos.Select(t => _mapper.Map<TodoItemDto>(t, opt =>
        {
            opt.Items["OwnerId"] = userId;
            opt.Items["UserManager"] = _userManager;
        })).ToList();
    }

    public async Task<bool> DeleteTodoAsync(int id, string userId)
    {
        var todo = await _unitOfWork.TodoItems.GetByIdAsync(id);
        if (todo == null || todo.UserId != userId) return false;

        _unitOfWork.TodoItems.Remove(todo);
        return await _unitOfWork.CompleteAsync() > 0;
    }
}
