using System;
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
        Task<bool> Any(Expression<Func<Product, bool>> predicate);
        Task<Product?> GetById(Guid id);
        Task<bool> Create(Product product);
        Task<Product> GetForEdit(Guid id);
        Task<bool> Update(Guid id, Product model);
        Task<bool> Delete(Guid id);
    }
}
