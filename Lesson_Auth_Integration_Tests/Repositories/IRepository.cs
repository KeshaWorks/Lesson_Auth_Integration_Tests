<<<<<<< HEAD
﻿namespace Lesson_Auth_Integration_Tests.Repositories;

=======
﻿// ====================================================================================================
// Repositories/IIRepository.cs
// ====================================================================================================
namespace Lesson_Auth_Integration_Tests.Repositories;

// SOLID: Interface Segregation
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindByAsync(Func<T, bool> predicate);
    Task AddAsync(T entity);
    void Remove(T entity);
}
