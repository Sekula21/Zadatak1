using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class ProductSortingService : IProductSortingService
    {
        public IEnumerable<Product> Sort(IEnumerable<Product> products, string sortOrder)
        {
            return sortOrder switch
            {
                "name_desc" => products.OrderByDescending(p => GetFirstChars(p.Name)),
                "type" => products.OrderBy(p => GetFirstChars(p.TypeOfHoney)),
                "type_desc" => products.OrderByDescending(p => GetFirstChars(p.TypeOfHoney)),
                "price" => products.OrderBy(p => p.PricePerJar),
                "price_desc" => products.OrderByDescending(p => p.PricePerJar),
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
    }
}
