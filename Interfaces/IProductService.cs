using System.Linq.Expressions;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface IProductService
    {
        Task<ProductFilterViewModel> ApplyFilters(ProductFilterViewModel filters);
        IEnumerable<Product> Sort(IEnumerable<Product> products, string sortOrder);
        Task<IEnumerable<Product>> GetAll();
        Task<Product?> GetById(Guid id);
        Task<bool> Any(Expression<Func<Product, bool>> predicate);

        void Update(Product product);
        void Delete(Product product);
        Task Add(Product product);
        Task SaveChanges();
    }
}
