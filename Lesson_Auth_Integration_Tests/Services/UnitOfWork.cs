<<<<<<< HEAD
﻿using Lesson_Auth_Integration_Tests.Interfaces;
=======
﻿// ====================================================================================================
// Services/AuthService.cs
// ====================================================================================================

using Lesson_Auth_Integration_Tests.Interfaces;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
using Lesson_Auth_Integration_Tests.Models;
using Lesson_Auth_Integration_Tests.Persistence;
using Lesson_Auth_Integration_Tests.Repositories;

namespace Lesson_Auth_Integration_Tests.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<TodoItem>? _todoItems;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IRepository<TodoItem> TodoItems =>
        _todoItems ??= new EfRepository<TodoItem>(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}