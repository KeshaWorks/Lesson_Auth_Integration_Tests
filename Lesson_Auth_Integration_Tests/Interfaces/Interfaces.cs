using Lesson_Auth_Integration_Tests.Models;
using Lesson_Auth_Integration_Tests.Repositories;

namespace Lesson_Auth_Integration_Tests.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<TodoItem> TodoItems { get; }
    Task<int> CompleteAsync();
}