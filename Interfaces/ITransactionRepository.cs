using Microsoft.EntityFrameworkCore;
using Zadatak1.Models;

namespace Zadatak1.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAll();
        Task CreateTransaction(User buyer, Product product, int amount);

    }
}
