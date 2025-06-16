using System.Linq.Expressions;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _userRepository;

        public AdminService(IRepository<Product> productRepository, IRepository<User> userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdProductsAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<bool> AnyProductsAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _productRepository.AnyAsync(predicate);
        }

        public void UpdateProducts(Product product)
        {
            _productRepository.Update(product);
        }

        public void DeleteProducts(Product product)
        {
            _productRepository.Delete(product);
        }

        public async Task AddProductsAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task SaveChangesProductsAsync()
        {
            await _productRepository.SaveChangesAsync();
        }
    }
}
