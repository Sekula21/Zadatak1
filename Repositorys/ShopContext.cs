using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zadatak1.Models;

namespace Zadatak1.Repositorys
{
    public class ShopContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Product> Products { get; set; }

        public ShopContext(DbContextOptions options) : base(options)
        {

        }


    }
}
