using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Repositorys
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ShopContext context) : base(context) { }

        public async Task<IEnumerable<Transaction>> GetAll() => await _context.Transactions.ToListAsync();
    }
}