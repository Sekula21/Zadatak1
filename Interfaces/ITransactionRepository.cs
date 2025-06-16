using Zadatak1.Models;

namespace Zadatak1.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<User?> GetUserById(Guid id);
        Task<Product?> GetProductById(Guid id);
        void UpdateProduct(Product product);

    }
}
