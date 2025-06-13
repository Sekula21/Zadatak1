using System.Linq.Expressions;

namespace Zadatak1.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
