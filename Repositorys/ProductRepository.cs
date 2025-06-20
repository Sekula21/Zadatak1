using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Repositorys
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopContext context) : base(context) { }
        public async Task<Product?> GetById(Guid id) => await _context.Products.FindAsync(id);

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<bool> Create(Product model)
        {
            if (model == null) return false;

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Type = model.Type,
                Producer = model.Producer,
                Adress = model.Adress,
                Color = model.Color,
                Description = model.Description,
                Price = model.Price,
                Amount = model.Amount
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;
        }
       

        public async Task<Product?> GetForEdit(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> Update(Guid id, Product model)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.Name = model.Name;
            product.Type = model.Type;
            product.Producer = model.Producer;
            product.Adress = model.Adress;
            product.Color = model.Color;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Amount = model.Amount;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
