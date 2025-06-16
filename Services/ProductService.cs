using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Services;
public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;

    public ProductService(IRepository<Product> productRepository)
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
        var products = await _productRepository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
            products = products.Where(p => p.Name.Contains(filters.SearchTerm, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filters.Type))
            products = products.Where(p => p.Type.Contains(filters.Type, StringComparison.OrdinalIgnoreCase));

        if (filters.MinPrice.HasValue)
            products = products.Where(p => p.Price >= filters.MinPrice.Value);

        if (filters.MaxPrice.HasValue)
            products = products.Where(p => p.Price <= filters.MaxPrice.Value);

        products = Sort(products, filters.SortOrder);
        filters.Products = products;

        return filters;
    }
}