using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Repositorys
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ShopContext context) : base(context) { }

        public async Task<IEnumerable<Transaction>> GetAll() => await _context.Transactions.ToListAsync();

        public async Task CreateTransaction(User buyer, Product product, int amount)
        {
            var transaction = new Transaction
            {
                Buyer = buyer,
                Product = product,
                TransactionDate = DateTime.Now,
                Amount = amount,
                Price = product.Price * amount
            };

            product.Amount -= amount;

            await _context.Transactions.AddAsync(transaction);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}