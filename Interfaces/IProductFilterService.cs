using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface IProductFilterService
    {
   //     IEnumerable<Product> ApplyFilters(IEnumerable<Product> products, ProductFilterViewModel filters);
        Task<ProductFilterViewModel> ApplyFiltersAsync(ProductFilterViewModel filters);
    }
}
