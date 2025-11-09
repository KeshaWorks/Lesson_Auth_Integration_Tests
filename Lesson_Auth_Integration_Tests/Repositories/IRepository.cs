namespace Lesson_Auth_Integration_Tests.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindByAsync(Func<T, bool> predicate);
    Task AddAsync(T entity);
    void Remove(T entity);
}
