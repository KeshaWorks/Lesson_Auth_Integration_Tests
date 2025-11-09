<<<<<<< HEAD
﻿using Lesson_Auth_Integration_Tests.Models;
=======
﻿// ====================================================================================================
// Interfaces/IAuthService.cs
// ====================================================================================================
using Lesson_Auth_Integration_Tests.Models;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
using Lesson_Auth_Integration_Tests.Repositories;

namespace Lesson_Auth_Integration_Tests.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<TodoItem> TodoItems { get; }
    Task<int> CompleteAsync();
}