using Zadatak1.Models;

namespace Zadatak1.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetById(Guid id);
        void Update(Product product);
        Task<bool> Create(Product product);
        Task<Product> GetForEdit(Guid id);
        Task<bool> Update(Guid id, Product model);
        Task<bool> Delete(Guid id);

    }
}
