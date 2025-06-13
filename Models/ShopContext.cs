using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Zadatak1.Models
{
    public class ShopContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Product> Products { get; set; }

        public ShopContext(DbContextOptions options) : base(options) {
        
        }


    }
}
