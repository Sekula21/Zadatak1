using System.Linq.Expressions;
using Zadatak1.Models;

namespace Zadatak1.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetByIdProductsAsync(Guid id);
        Task<bool> AnyProductsAsync(Expression<Func<Product, bool>> predicate);

        void UpdateProducts(Product product);
        void DeleteProducts(Product product);
        Task AddProductsAsync(Product product);
        Task SaveChangesProductsAsync();
    }
}
