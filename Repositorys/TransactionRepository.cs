using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Repositorys
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ShopContext context) : base(context) { }

        public async Task<User?> GetUserByIdAsync(Guid id) => await _context.Users.FindAsync(id);
        public async Task<Product?> GetProductByIdAsync(Guid id) => await _context.Products.FindAsync(id);

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
