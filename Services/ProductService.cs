using System.Linq.Expressions;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.Repositorys;
using Zadatak1.ViewModels;

namespace Zadatak1.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<Product> Sort(IEnumerable<Product> products, string sortOrder)
    {
        return sortOrder switch
        {
            "name_desc" => products.OrderByDescending(p => GetFirstChars(p.Name)),
            "type" => products.OrderBy(p => GetFirstChars(p.Type)),
            "type_desc" => products.OrderByDescending(p => GetFirstChars(p.Type)),
            "price" => products.OrderBy(p => p.Price),
            "price_desc" => products.OrderByDescending(p => p.Price),
            _ => products.OrderBy(p => GetFirstChars(p.Name)),
        };
    }
    private static string GetFirstChars(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return value.Length <= 3 ? value : value.Substring(0, 3);
    }

    public async Task<ProductFilterViewModel> ApplyFilters(ProductFilterViewModel filters)
    {
        var products = await _productRepository.GetAll();

        if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
            products = products.Where(p => p.Name.Contains(filters.SearchTerm, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filters.Type))
            products = products.Where(p => p.Type.Contains(filters.Type, StringComparison.OrdinalIgnoreCase));

        if (filters.MinPrice.HasValue)
            products = products.Where(p => p.Price >= filters.MinPrice.Value);

        if (filters.MaxPrice.HasValue)
            products = products.Where(p => p.Price <= filters.MaxPrice.Value);

        products = Sort(products, filters.SortOrder);

        filters.Products = (IEnumerable<ProductEditViewModel>)products;

        return filters;
    }


    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _productRepository.GetAll();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _productRepository.GetById(id);
    }

    public async Task<bool> Any(Expression<Func<Product, bool>> predicate)
    {
        return await _productRepository.Any(predicate);
    }

    public async Task<bool> Create(Product model)
    {
        return await _productRepository.Create(model);
    }

    public async Task<Product?> GetForEdit(Guid id)
    {
        return await _productRepository.GetForEdit(id);
    }

    public async Task<bool> Update(Guid id, Product updatedProduct)
    {
        return await _productRepository.Update(id, updatedProduct);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _productRepository.Delete(id);
    }

    public async Task<bool> ProductExists(Guid id)
    {
        return await _productRepository.Any(p => p.Id == id);
    }

}