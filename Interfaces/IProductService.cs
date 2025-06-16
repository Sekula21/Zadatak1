using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface IProductService
    {
        Task<ProductFilterViewModel> ApplyFilters(ProductFilterViewModel filters);
        IEnumerable<Product> Sort(IEnumerable<Product> products, string sortOrder);
    }
}
