using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Services;
public class ProductFilterService : IProductFilterService
{
    private readonly IProductSortingService _sortingService;
    private readonly IRepository<Product> _productRepository;

    public ProductFilterService(IProductSortingService sortingService, IRepository<Product> productRepository)
    {
        _sortingService = sortingService;
        _productRepository = productRepository;
    }

    public async Task<ProductFilterViewModel> ApplyFiltersAsync(ProductFilterViewModel filters)
    {
        var products = await _productRepository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
            products = products.Where(p => p.Name.Contains(filters.SearchTerm, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filters.TypeOfHoney))
            products = products.Where(p => p.TypeOfHoney.Contains(filters.TypeOfHoney, StringComparison.OrdinalIgnoreCase));

        if (filters.MinPrice.HasValue)
            products = products.Where(p => p.PricePerJar >= filters.MinPrice.Value);

        if (filters.MaxPrice.HasValue)
            products = products.Where(p => p.PricePerJar <= filters.MaxPrice.Value);

        products = _sortingService.Sort(products, filters.SortOrder);
        filters.Products = products;

        return filters;
    }
}