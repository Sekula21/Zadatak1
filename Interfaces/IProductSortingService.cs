using Zadatak1.Models;

namespace Zadatak1.Interfaces
{
    public interface IProductSortingService
    {
        IEnumerable<Product> Sort(IEnumerable<Product> products, string sortOrder);
    }
}
