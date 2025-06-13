using Zadatak1.Models;

namespace Zadatak1.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<Product?> GetProductByIdAsync(Guid id);
        void UpdateProduct(Product product);

    }
}
