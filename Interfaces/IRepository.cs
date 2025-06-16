using System.Linq.Expressions;

namespace Zadatak1.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChanges();
        Task<bool> Any(Expression<Func<T, bool>> predicate);
    }
}
